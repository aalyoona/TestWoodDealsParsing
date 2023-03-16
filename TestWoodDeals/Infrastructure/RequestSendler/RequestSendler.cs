using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TestWoodDeals.Helpers;
using TestWoodDeals.Models;

namespace TestWoodDeals
{
    public class RequestSendler : IRequestSendler
    {
        private readonly IWoodDealsService _woodDealsService;

        public RequestSendler(IWoodDealsService woodDealsService)
        {
            _woodDealsService = woodDealsService;
        }

        public void SendRequest()
        {
            PostRequest postRequest = new PostRequest("https://www.lesegais.ru/open-area/graphql");

            CookieContainer cookieContainer = new CookieContainer();

            //Fiddler's proxy
            WebProxy webProxy = new WebProxy("127.0.0.1", 8888);
            postRequest.Proxy = webProxy;

            //query data to get information about how many records are in the table and how many are displayed on one page
            postRequest.Data = "{\"query\":\"query SearchReportWoodDealCount($size: Int!, $number: Int!, $filter: Filter," +
                " $orders: [Order!]) {\\n searchReportWoodDeal(filter: $filter, pageable: { number: $number, size: $size}, " +
                "orders: $orders) {\\n total\\n number\\n size\\n overallBuyerVolume\\n overallSellerVolume\\n __typename\\n  }" +
                "\\n}\\n\",\"variables\":{\"size\":20,\"number\":0,\"filter\":null},\"operationName\":\"SearchReportWoodDealCount\"}";

            //adding all query headers
            postRequest.Accept = "*/*";
            postRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36";
            postRequest.ContentType = "application/json";
            postRequest.Referer = "https://www.lesegais.ru/open-area/deal";
            postRequest.Host = "www.lesegais.ru";
            postRequest.Headers.Add("Origin", "https://www.lesegais.ru");
            postRequest.Headers.Add("sec-ch-ua", "\".Not / A)Brand\";v=\"99\", \"Google Chrome\";v=\"103\", \"Chromium\";v=\"103\"");
            postRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            postRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            postRequest.Headers.Add("Sec-Fetch-Dest", "empty");
            postRequest.Headers.Add("Sec-Fetch-Mode", "cors");
            postRequest.Headers.Add("Sec-Fetch-Site", "same-origin");

            //sending a request
            postRequest.Run(cookieContainer);

            //парсинг ответа
            JObject json = JObject.Parse(postRequest.Response.ToString());
            int total = Convert.ToInt32((json["data"].Children().Children().Values<string>("total").ToList())[0]);
            int size = Convert.ToInt32((json["data"].Children().Children().Values<string>("size").ToList())[0]);
            int number = (int)Math.Round((double)total / size);
            int j = 1;

            //loop for traversal page by page
            for (int i = 0; i < number; i++)
            {
                //request data to get only one page
                postRequest.Data = "{\"query\":\"query SearchReportWoodDeal($size: Int!, $number: Int!, $filter: Filter," +
                " $orders: [Order!]) {\\n searchReportWoodDeal(filter: $filter, pageable: { number: $number, size: $size}," +
                " orders: $orders) {\\n content {\\n sellerName\\n sellerInn\\n buyerName\\n buyerInn\\n woodVolumeBuyer\\n" +
                " woodVolumeSeller\\n dealDate\\n dealNumber\\n __typename\\n    }\\n __typename\\n  }\\n}\\n\",\"variables\":{" +
                $" \"size\":{size},\"number\":{i}, " +
                "\"filter\":null,\"orders\":null},\"operationName\":\"SearchReportWoodDeal\"}";

                //sending a request
                postRequest.Run(cookieContainer);

                //de-realization of the response and adding objects
                RootResponseModel root = JsonConvert.DeserializeObject<RootResponseModel>(postRequest.Response.ToString());

                List<ContentModel> reportWoodDealResponceModels = root.Data.SearchReportWoodDeal.Content;

                foreach (ContentModel contentResponceModel in reportWoodDealResponceModels)
                {
                    contentResponceModel.Id = j;
                    j++;
                    _woodDealsService.AddWoodDeals(contentResponceModel);
                }
            }
        }
    }
}

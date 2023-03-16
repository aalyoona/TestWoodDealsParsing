using Newtonsoft.Json;

namespace TestWoodDeals.Models
{
    public class DataResponseModel
    {
        [JsonProperty("searchReportWoodDeal")]
        public SearchReportWoodDealResponseModel SearchReportWoodDeal { get; set; }
    }
}
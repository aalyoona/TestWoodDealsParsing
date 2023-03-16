using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestWoodDeals.Models
{
    public class SearchReportWoodDealResponseModel
    {
        [JsonProperty("content")]
        public List<ContentModel> Content { get; set; }

        [JsonProperty("__typename")]
        public string Typename { get; set; }
    }
}

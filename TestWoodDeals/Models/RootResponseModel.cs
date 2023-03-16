using Newtonsoft.Json;

namespace TestWoodDeals.Models
{
    public class RootResponseModel
    {
        [JsonProperty("data")]
        public DataResponseModel Data { get; set; }
    }
}

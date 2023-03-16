using Newtonsoft.Json;

namespace TestWoodDeals.Models
{
    public class ContentModel
    {
        public int Id { get; set; }

        [JsonProperty("sellerName")]
        public string SellerName { get; set; }

        [JsonProperty("sellerInn")]
        public string SellerInn { get; set; }

        [JsonProperty("buyerName")]
        public string BuyerName { get; set; }

        [JsonProperty("buyerInn")]
        public string BuyerInn { get; set; }

        [JsonProperty("woodVolumeBuyer")]
        public double WoodVolumeBuyer { get; set; }

        [JsonProperty("woodVolumeSeller")]
        public double WoodVolumeSeller { get; set; }

        [JsonProperty("dealDate")]
        public string DealDate { get; set; }

        [JsonProperty("dealNumber")]
        public string DealNumber { get; set; }

        [JsonProperty("__typename")]
        public string Typename { get; set; }
    }
}

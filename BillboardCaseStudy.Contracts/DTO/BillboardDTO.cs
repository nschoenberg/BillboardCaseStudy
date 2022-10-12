using Newtonsoft.Json;

namespace BillboardCaseStudy.Contracts.DTO
{
    public class BillboardDto
    {
        public BillboardDto()
        {
            No = string.Empty;
        }

        [JsonProperty("Tafel__AUNr")]
        public string No { get; set; }

        [JsonProperty("Tafel__Preis")]
        public decimal Price { get; set; }

        [JsonProperty("Tafel__BreiteReal")]
        public double Latidude { get; set; }

        [JsonProperty("Tafel__LaengeReal")]
        public double Longitude { get; set; }
    }
}

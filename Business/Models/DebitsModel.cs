using System.Text.Json.Serialization;

namespace Business.Models
{
    public class DebitsModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CardModel>? Cards { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<PurchaseModel>? Purchases { get; set; }         
    }
}
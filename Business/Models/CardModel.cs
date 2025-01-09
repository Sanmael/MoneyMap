using System.Text.Json.Serialization;

namespace Business.Models
{
    public class CardModel
    {
        public Guid Id { get; set; }
        public UserModel? User { get; set; }
        public Guid UserId { get; set; }
        public Guid CategorieId { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public PurchaseCategorieModel? Categorie { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<PurchaseInInstallmentsModel>? PurchaseInInstallmentsModels { get; set; }
    }
}
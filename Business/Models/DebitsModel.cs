namespace Business.Models
{
    public class DebitsModel
    {        
        public List<CardModel>? Cards { get; set; }
        public List<PurchaseModel>? Purchases { get; set; }         
    }
}
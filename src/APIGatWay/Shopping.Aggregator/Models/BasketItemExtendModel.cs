namespace Shopping.Aggregator.Models
{
    public class BasketItemExtendModel
    {
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        
        // Additional Information.
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
    }
}
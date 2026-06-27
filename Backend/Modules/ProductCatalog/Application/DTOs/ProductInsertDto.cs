namespace Backend.Modules.ProductCatalog.Application.DTOs
{
    public class ProductInsertDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
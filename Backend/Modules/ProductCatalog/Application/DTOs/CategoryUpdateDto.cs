namespace Backend.Modules.ProductCatalog.Application.DTOs
{

    public class CategoryUpdateDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool Active { get; set; }
    }


}
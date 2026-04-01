namespace Nexus.Models.Products
{
    public class ProductsDetailsViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Quality { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
    }
}

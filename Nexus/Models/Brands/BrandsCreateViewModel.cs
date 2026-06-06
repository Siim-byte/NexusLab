using Microsoft.AspNetCore.Http;
namespace Nexus.Models.Brands

{
    public class BrandsCreateViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public IFormFile LogoFile { get; set; }
    }
}

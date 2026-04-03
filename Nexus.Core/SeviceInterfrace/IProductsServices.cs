using Nexus.Core.Dto;
using Nexus.Nexus.Core.Domain;

namespace Nexus.Core.SeviceInterfrace
{
    public interface IProductsServices
    {
        Task<Product> Create(ProductsDTO dto);
        Task<Product> DetailsAsync(Guid id);
        Task<Product> Delete(Guid id);
        Task<Product> Update(ProductsDTO dto);
    }
}

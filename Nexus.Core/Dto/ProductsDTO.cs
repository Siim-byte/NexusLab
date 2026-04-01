using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Core.Dto
{
    public class ProductsDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Quality { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }

        
    }
}

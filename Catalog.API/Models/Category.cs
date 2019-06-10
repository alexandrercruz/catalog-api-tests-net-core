using System.Collections.Generic;

namespace Catalog.API.Models
{
    public class Category : Entity
    {
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
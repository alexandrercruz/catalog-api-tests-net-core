using System;

namespace Catalog.API.ViewModels.ProductViewModel
{
    public class ListProductViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string Category { get; set; }
    }
}
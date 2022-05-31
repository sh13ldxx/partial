using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.ViewModel.Products
{
    public class ProductVm
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string Category { get; internal set; }
        public string Image { get; internal set; }
        public double Price { get; internal set; }
        public bool IsDeleted { get; internal set; }

    }
}

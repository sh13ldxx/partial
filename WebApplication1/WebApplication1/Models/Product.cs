using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int StockCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public int Rating { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public Category Category { get; set; }
        public List<ProdutcImage> ProdutcImages { get; set; }

    }
}

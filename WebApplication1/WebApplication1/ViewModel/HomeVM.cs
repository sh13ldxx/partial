using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class HomeVM
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public List<ProdutcImage> ProdutcImages { get; set; }
        public List<Slider> Sliders { get; set; }


    }
}

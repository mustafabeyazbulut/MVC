using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DouaCosmeticShopping.Models
{
    public class ProductViewModel
    {
        public int P_ID { set; get; }
        public string P_Name { set; get; }
        public int P_Quantity { set; get; }
        public int P_Price { set; get; }
        public string P_Description { set; get; }

        public List<ProductViewModel> ProductList { get; set; }

        public int P_ID2 { set; get; }
    }
}
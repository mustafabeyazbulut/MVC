using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DouaCosmeticShopping.Models
{
    public class OrderViewModel
    {
        public List<OrderTableViewModel> OrderList { get; set; }
        public List<ProductTableViewModel> ProductList { get; set; }
        public List<OrderProductTableViewModel> OrderProductList { get; set; }

        public int productCount { set; get; }
        public int oID { set; get; }
    }
    public class ProductTableViewModel
    {
        public int P_ID { set; get; }
        public string P_Name { set; get; }
        public int P_Quantity { set; get; }
        public int P_Price { set; get; }
        public string P_Description { set; get; }
    }
    public class OrderTableViewModel
    {
        public int O_ID { set; get; }
        public int C_ID { set; get; }
        public DateTime O_Created_at { set; get; }
        public bool O_Status { set; get; }



    }
    public class OrderProductTableViewModel
    {
        public int OP_ID { set; get; }
        public int OP_Quantity { set; get; }
        public int OP_O_ID { set; get; }
        public int OP_P_ID { set; get; }

        public int P_Price { set; get; }
        public string P_Name { set; get; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DouaCosmeticShopping.Models
{
    public class CustomerViewModel
    {
        public int C_ID { set; get; }
        public string C_Email { set; get; }
        public string C_FirstName { set; get; }
        public string C_LastName { set; get; }
        public string C_PhoneNumber { set; get; }
        public string C_Adress { set; get; }
        public List<CustomerViewModel> CustomerList { get; set; }
    }
}
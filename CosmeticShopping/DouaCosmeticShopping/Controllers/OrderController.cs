using DouaCosmeticShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DouaCosmeticShopping.Controllers
{
    public class OrderController : Controller
    {
        DouaCosmeticShoppingEntities db = new DouaCosmeticShoppingEntities();
        // GET: Order
        public ActionResult Index(OrderViewModel model)
        {
           
            model.ProductList = (from x in db.Table_Product
                                 select new ProductTableViewModel
                                 {
                                     P_ID = x.P_ID,
                                     P_Name = x.P_Name,
                                     P_Quantity = (int)x.P_Quantity,
                                     P_Price = (int)x.P_Price,
                                     P_Description = x.P_Description
                                 }
                                 ).OrderByDescending(k => k.P_ID).ToList();
            model.productCount =0;

            model.OrderList = (from x in db.Table_Order
                               where x.O_Status==false
                                 select new OrderTableViewModel
                                 {
                                     O_ID = x.O_ID,
                                     C_ID = (int)x.C_ID,
                                     O_Created_at = (DateTime)x.O_Created_at,
                                     O_Status = (bool)x.O_Status,
                                 }
                                ).ToList();
            

            var order = (from x in db.Table_Order
                         where x.O_Status == false && x.C_ID==1
                         select x).FirstOrDefault();

            if (order == null)
            {
                Table_Order newOrder = new Table_Order();

                newOrder.C_ID = 1;//kullanıcıid
                newOrder.O_Created_at = DateTime.Now;
                newOrder.O_Status = false;
                db.Table_Order.Add(newOrder);
                db.SaveChanges();
                order = newOrder;
            }
            model.OrderProductList = (from x in db.Table_OrderProduct
                                      where x.O_ID == order.O_ID
                                      select new OrderProductTableViewModel
                                      {
                                          OP_ID = x.OP_ID,
                                          OP_Quantity = (int)x.OP_Quantity,
                                          OP_O_ID = (int)x.O_ID,
                                          OP_P_ID = (int)x.P_ID,
                                          P_Price = (int)x.Table_Product.P_Price,
                                          P_Name = x.Table_Product.P_Name,
                                      }
                                ).ToList();
            model.productCount = model.OrderProductList.Count;

            model.oID=order.O_ID;

            if (Request.IsAjaxRequest()) // index sayfası ilk defa açılmadıysa sadece tabloyu günceller
            {
                return PartialView("_TableTwo", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public string AddCard(int id)
        {
            var order = (from x in db.Table_Order
                         where x.O_Status == false && x.C_ID == 1
                         select x).FirstOrDefault(); // kullanıcıya ait order kontrolü yapar
            
            var queryOP = (from x in db.Table_OrderProduct
                           where x.P_ID == id && x.O_ID == order.O_ID
                           select x).FirstOrDefault(); // ordera ait eklenmek istenen ürün kontrolü yapar
            if (queryOP != null) // ürün daha önce eklendiyse sayıyıy arttırır.
            {
                queryOP.OP_Quantity += 1;
                db.SaveChanges();
            }
            else // ürün eklenmediyse 1 adet eklenir
            {
                Table_OrderProduct newOP = new Table_OrderProduct();

                newOP.OP_Quantity = 1;
                newOP.O_ID = order.O_ID; //orderid
                newOP.P_ID = id;
                db.Table_OrderProduct.Add(newOP);
                db.SaveChanges();
            }
          

            return "1"; // yeni kayıt oluştuysa 1 döner

        }
        [HttpPost]
        public string EmptyCard(int id)
        {
            var queryOP = (from x in db.Table_OrderProduct
                           where x.O_ID == id
                           select x).ToList(); // ordera ait eklenmek istenen ürün kontrolü yapar


            foreach (var data in queryOP)
            {
                db.Table_OrderProduct.Remove(data);
            }
            db.SaveChanges();


            return "1"; // yeni kayıt oluştuysa 1 döner

        }
        [HttpPost]
        public string CheckOut(int id)
        {
            var order = (from x in db.Table_Order
                         where x.O_Status == false && x.O_ID == id
                         select x).FirstOrDefault(); // kullanıcıya ait order kontrolü yapar


           
            if (order != null) // ürün daha önce eklendiyse sayıyıy arttırır.
            {
                order.O_Status =true;
                db.SaveChanges();
            }
            

            return "1"; // yeni kayıt oluştuysa 1 döner

        }




    }
}
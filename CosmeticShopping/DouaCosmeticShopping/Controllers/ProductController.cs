using DouaCosmeticShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DouaCosmeticShopping.Controllers
{
    public class ProductController : Controller
    {
        DouaCosmeticShoppingEntities db = new DouaCosmeticShoppingEntities();
        // GET: Product
        public ActionResult Index(ProductViewModel model)
        {
            model.ProductList = (from x in db.Table_Product
                                 select new ProductViewModel
                                 {
                                     P_ID=x.P_ID,
                                     P_Name=x.P_Name,
                                     P_Quantity= (int)x.P_Quantity,
                                     P_Price = (int)x.P_Price,
                                     P_Description=x.P_Description
                                 }                              
                                 ).OrderByDescending(k => k.P_ID).ToList();

            if (Request.IsAjaxRequest()) // index sayfası ilk defa açılmadıysa sadece tabloyu günceller
            {
                return PartialView("_Table", model);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public string AddProduct(ProductViewModel model)
        {
            try
            {

                if (model.P_ID2 == 0) // yeni ekleme işleminde P_ID 0 dır.
                {
                    Table_Product newProduct = new Table_Product();
                    newProduct.P_Name = model.P_Name;
                    newProduct.P_Quantity = model.P_Quantity;
                    newProduct.P_Price = model.P_Price;
                    newProduct.P_Description = model.P_Description;

                    db.Table_Product.Add(newProduct);
                    db.SaveChanges();
                    return "1"; // successful insertion
                }
                else // geri kalan durumlarda güncelleme söz konusudur.
                {

                    Table_Product queryProduct = db.Table_Product.FirstOrDefault(f => f.P_ID == model.P_ID2);
                    queryProduct.P_Name = model.P_Name;
                    queryProduct.P_Quantity = model.P_Quantity;
                    queryProduct.P_Price = model.P_Price;
                    queryProduct.P_Description = model.P_Description;
                    db.SaveChanges();
                    return "2"; // successful update
                }

            }
            catch
            {
                return "-1"; //error
            }
        }

        [HttpPost]
        public JsonResult GetProduct(int id)
        {
            //11

            Table_Product queryProduct = db.Table_Product.FirstOrDefault(f => f.P_ID == id);


            ProductViewModel Pmodel = new ProductViewModel();

            Pmodel.P_ID = queryProduct.P_ID;
            Pmodel.P_Name = queryProduct.P_Name;
            Pmodel.P_Quantity = (int)queryProduct.P_Quantity;
            Pmodel.P_Price = (int)queryProduct.P_Price;
            Pmodel.P_Description = queryProduct.P_Description;

            return Json(Pmodel, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public string DeleteProduct(int id)
        {
            //11
            try
            {
                Table_OrderProduct queryOrderProduct=db.Table_OrderProduct.FirstOrDefault(f => f.P_ID == id);
                var queryProduct = db.Table_Product.FirstOrDefault(f => f.P_ID == id);
                if (queryOrderProduct != null)
                {
                    queryProduct.P_Quantity = 0;
                    db.SaveChanges();
                    return "2"; // Success
                }
                else
                {
                    db.Table_Product.Remove(queryProduct);
                    db.SaveChanges();
                    return "1"; // Success
                }
                
                
            }  
            catch
            {
                return "-1"; //error
            }

        }


    }
}
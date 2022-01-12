using DouaCosmeticShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DouaCosmeticShopping.Controllers
{
    public class CustomerController : Controller
    {
        DouaCosmeticShoppingEntities db = new DouaCosmeticShoppingEntities();
        // GET: Customer
        public ActionResult Index(CustomerViewModel model)
        {
            model.CustomerList = (from x in db.Table_Customer
                                 select new CustomerViewModel
                                 {
                                     C_ID = x.C_ID,
                                     C_Email = x.C_Email,
                                     C_FirstName = x.C_FirstName,
                                     C_LastName = x.C_LastName,
                                     C_PhoneNumber = x.C_PhoneNumber,
                                     C_Adress =x.C_Adress
                                 }
                                ).OrderByDescending(k => k.C_ID).ToList();

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
        public string AddCustomer(CustomerViewModel model)
        {
            try
            {

                if (model.C_ID == 0) // yeni ekleme işleminde P_ID 0 dır.
                {
                    Table_Customer newCustomer = new Table_Customer();

                    newCustomer .C_ID = model.C_ID;
                    newCustomer.C_Email = model.C_Email;
                    newCustomer.C_FirstName = model.C_FirstName;
                    newCustomer.C_LastName = model.C_LastName;
                    newCustomer.C_PhoneNumber = model.C_PhoneNumber;
                    newCustomer.C_Adress = model.C_Adress;
                    

                    db.Table_Customer.Add(newCustomer);
                    db.SaveChanges();
                    return "1"; // successful insertion
                }
                else // geri kalan durumlarda güncelleme söz konusudur.
                {

                    Table_Customer queryCustomer = db.Table_Customer.FirstOrDefault(f => f.C_ID == model.C_ID);

                    queryCustomer.C_ID = model.C_ID;
                    queryCustomer.C_Email = model.C_Email;
                    queryCustomer.C_FirstName = model.C_FirstName;
                    queryCustomer.C_LastName = model.C_LastName;
                    queryCustomer.C_PhoneNumber = model.C_PhoneNumber;
                    queryCustomer.C_Adress = model.C_Adress;
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
        public JsonResult GetCustomer(int id)
        {
            //11

            Table_Customer queryCustomer = db.Table_Customer.FirstOrDefault(f => f.C_ID ==id);


            CustomerViewModel Cmodel = new CustomerViewModel();

            Cmodel.C_ID = queryCustomer.C_ID;
            Cmodel.C_Email = queryCustomer.C_Email;
            Cmodel.C_FirstName = queryCustomer.C_FirstName;
            Cmodel.C_LastName = queryCustomer.C_LastName;
            Cmodel.C_PhoneNumber = queryCustomer.C_PhoneNumber;
            Cmodel.C_Adress = queryCustomer.C_Adress;

            return Json(Cmodel, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public string DeleteCustomer(int id)
        {
            //11
            try
            {
                Table_Order queryOrder = db.Table_Order.FirstOrDefault(f => f.C_ID == id);
                Table_Customer queryCustomer = db.Table_Customer.FirstOrDefault(f => f.C_ID == id);
                if (queryOrder != null)
                {
                  
                    return "2"; // Error
                }
                else
                {
                    db.Table_Customer.Remove(queryCustomer);
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
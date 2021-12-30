using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzdilekBtServisTakipPortalı.Controllers;

namespace OzdilekBtServisTakipPortalı.Models.Giris
{
    public class LoginState
    {
        private Models.OzdilekBtServisTakipEntities db;
        public LoginState()
        {

            db = new Models.OzdilekBtServisTakipEntities();
        }
        // Giriskontrollerden çektiği username ve passwordu alıp 
        //doğruysa resultuser'a atar ve songiristarih güncelleyip Kullanıyı yetkiyi sessiona ekler
        //Daha sonrasında httpcontext çekmiş olan Controllogin sayfasına gider sonra Controlloginde kaldığı yerden devam ederse returne truedan devam eder
        //return true ise girişe gider
        public bool IsLoginSuccess(string user, string pass)
        {

            KullaniciTb resultUser = db.KullaniciTb.Where(x => x.KullaniciAdi.Equals(user) && x.Sifre.Equals(pass)).FirstOrDefault();
            if (resultUser != null)
            {

                //resultUser boş değilse buraya girer
                resultUser.SonGirisTarihi = DateTime.Now.ToString();
                db.Entry(resultUser).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                //Httpcontext ile veritabanına giriş yapan kullanıcı bilgileri session içine kayıt olur
                HttpContext.Current.Session.Add("UserID", resultUser.KullaniciYetki.ToString());
                HttpContext.Current.Session.Add("KulAdi", resultUser.KullaniciAdi.ToString());
                HttpContext.Current.Session.Add("KulID", resultUser.KullaniciID.ToString());
                //
                return true;
            }
            return false;

        }
      
    }
}
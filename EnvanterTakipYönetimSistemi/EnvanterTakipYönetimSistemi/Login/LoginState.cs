using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnvanterTakipYönetimSistemi.Models;

namespace EnvanterTakipYönetimSistemi.Login
{
    public class LoginState
    {
        private Models.EnvanterServisEntities db;

        public LoginState()
        {
            db = new Models.EnvanterServisEntities();
        }

        // LoginController'dan buraya eposta ve parola bilgisi gelir.
        // resultpersonel değişkenine eposta ve parola doğruysa o personeli çeker.
        // Personel bilgilerini Session'a ekler ve geriye return true değerini döner
        public bool IsLoginSuccess(string eposta, string parola)
        {


            Tbl_Personel resultPersonel=db.Tbl_Personel.Where(x => x.Per_Eposta.Equals(eposta) && x.Per_Sifre.Equals(parola)&&x.Per_Kayit==true).FirstOrDefault(); // Çalışma durumu true olan eposta ve parola bilgileri uyuşan personeli çeker.
            if (resultPersonel != null)//resultUser boş değilse buraya girer
            {

                resultPersonel.Per_SonGirisTarihi = DateTime.Now;
                db.Entry(resultPersonel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //Httpcontext ile veritabanına giriş yapan kullanıcı bilgileri session içine kayıt olur
                HttpContext.Current.Session.Add("Per_ID", resultPersonel.Per_ID);
                HttpContext.Current.Session.Add("Per_AdiSoyadi", resultPersonel.Per_Ad+" "+resultPersonel.Per_Soyad);
                HttpContext.Current.Session.Add("Per_eposta", resultPersonel.Per_Eposta);
                HttpContext.Current.Session.Add("Per_Rol", resultPersonel.Tbl_P_Rol.Rol_Value);
                //
                return true;
            }
            return false;

        }



    }
}
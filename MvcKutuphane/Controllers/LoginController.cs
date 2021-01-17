using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcKutuphane.Models.Entity;
using System.Web.Security;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class LoginController : Controller
    {
        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities(); //bağlantı
        // GET: Login
        public ActionResult GirişYap()
        {
            return View();
        }
        //Giriş Postu
        [HttpPost]
        public ActionResult GirişYap(TBLUYELER p)
        {
            //Mail ve Şifre Kontrolü
            var bilgiler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == p.MAIL && x.SIFRE == p.SIFRE);
            //Session işlemleri
            Session["Mail"] = bilgiler.MAIL.ToString();
            //TempData["id"] = bilgiler.AD.ToString();
            //TempData["Ad"] = bilgiler.AD.ToString();
            //TempData["Soyad"] = bilgiler.SOYAD.ToString();
            //TempData["KullanıcıAdı"] = bilgiler.KULLANICIADI.ToString();
            //TempData["Sifre"] = bilgiler.SIFRE.ToString();
            //TempData["Fakulte"] = bilgiler.FAKULTE.ToString();

            //bilgiler boş değil ise
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);//yetkilendirme
                return RedirectToAction("Index","Panelim");
            }
            else
            {
                return View();
            }
          
        }
    }
}
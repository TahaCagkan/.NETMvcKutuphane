using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class PanelimController : Controller
    {
        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities();
        // GET: Panelim
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            var degerler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyemail);

            return View(degerler);
        }

        //Şifreyi Güncelleme
        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            //mail bilgilerini taşıycam Session'a
            var kullanici = (string)Session["Mail"];
            //kullanici dan gelen değerle tablomdaki MAIL'e göre işlem yapıcam
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == kullanici);
            uye.SIFRE = p.SIFRE;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Daha önce aldığım kitapları göster
        public ActionResult Kitaplarım()
        {
            //Session'a göre işlem yap
            var kullanici = (string)Session["Mail"];
            //Üyeler tablosunda mail adresindeki id si eşit olan kişiyi seç
            var id = db.TBLUYELER.Where(x => x.MAIL == kullanici.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(x => x.UYEID == id).ToList();
            return View(degerler);
        }

        //Logout işlemi
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirişYap","Login");
        }
    }
}
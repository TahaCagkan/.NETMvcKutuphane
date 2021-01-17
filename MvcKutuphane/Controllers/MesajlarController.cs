using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class MesajlarController : Controller
    {
        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities();//veritabanıma bağlantı
        // GET: Mesajlar
        //Gelen Mesajlar
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"].ToString();
            //mesajları listeleme
            var mesajlar = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail.ToString()).ToList();
            return View(mesajlar);
        }
        //Ginde Mesajlar
        public ActionResult Giden()
        {
            var uyemail = (string)Session["Mail"].ToString();
            //mesajları listeleme
            var mesajlar = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail.ToString()).ToList();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR t)
        {
            var uyemail = (string)Session["Mail"].ToString();
            t.GONDEREN = uyemail.ToString();
            t.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(t);
            db.SaveChanges();

            return RedirectToAction("Giden","Mesajlar");
        }
    }
}
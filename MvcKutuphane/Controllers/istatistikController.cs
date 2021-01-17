using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities();
        public ActionResult Index()
        {
            var deger1 = db.TBLUYELER.Count();
            var deger2 = db.TBLKITAP.Count();
            var deger3 = db.TBLKITAP.Where(x => x.DURUM == false).Count(); //emanet kitaplardaki durumu false ları göster
            var deger4 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;

            return View();
        }

        public ActionResult Galeri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/Resimler/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }

    }
}
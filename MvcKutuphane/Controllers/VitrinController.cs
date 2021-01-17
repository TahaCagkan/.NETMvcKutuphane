using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using MvcKutuphane.Models.Siniflarim;

namespace MvcKutuphane.Controllers
{
    public class VitrinController : Controller
    {
        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities();
        // GET: Vitrin
        [HttpGet]
        public ActionResult Index()
        {
            Listeler lste = new Listeler();
            lste.KitapListesi = db.TBLKITAP.ToList();//Kitap Listesi
            lste.HakkimizdaListesi = db.TBLHAKKIMIZDA.ToList();//Hakkımızda Listesi

            //var degerler = db.TBLKITAP.ToList();
            return View(lste);
        }

        [HttpPost]
        public ActionResult Index(TBLILETISIM iletism)
        {
            db.TBLILETISIM.Add(iletism);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
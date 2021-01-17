using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities();
        // GET: Odunc
        public ActionResult Index(string arancak)
        {
           

            var uyeler = from k in db.TBLHAREKET.Where(x => x.ISLEMDURUM == false) select k;
            //Null veya Boş değilse
            if (!string.IsNullOrEmpty(arancak))
            {
                //arancak değişkeninden aldığımız değeri kitaplar değişkenine at,daha sonra bunu da TBLKITAP içerisinde AD'a göre ara
                uyeler = uyeler.Where(m => m.TBLUYELER.ID.ToString().Contains(arancak));
            }


            //içermiyorsa Listeyi dön
            return View(uyeler.ToList());
        }
        //Ödünç verme işlemi GET
        [HttpGet]
        public ActionResult OduncVer()
        {
            //Üye Adını Listelemek için LINQ Sorgusu 
            List<SelectListItem> uyem = (from x in db.TBLUYELER.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.AD +' '+ x.SOYAD,
                                             Value = x.ID.ToString()

                                         }).ToList();

            ViewBag.uye1 = uyem;

            //Personel Adını Listelemek için LINQ Sorgusu 
            List<SelectListItem> peronelim = (from p in db.TBLPERSONEL.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = p.PERSONELAD + ' ' +p.PERSONELSOYAD,
                                                  Value = p.ID.ToString()
                                              }).ToList();

            ViewBag.prs1 = peronelim;
            return View();
        }
        //Ödünç Ver Post
        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET tblodunver)
        {
            //Üye ID yi çektik
            var uyeler = db.TBLUYELER.Where(k => k.ID == tblodunver.TBLUYELER.ID).FirstOrDefault();
            //Personel ID yi çektik
            var personeller = db.TBLPERSONEL.Where(y => y.ID == tblodunver.TBLPERSONEL.ID).FirstOrDefault();

            //atadığımız bilgileri değişkene ata
            tblodunver.TBLUYELER = uyeler;
            tblodunver.TBLPERSONEL = personeller;


            db.TBLHAREKET.Add(tblodunver);
            db.SaveChanges();

            return RedirectToAction("OduncVer");
        }

        //Kitabın Geri Alınası Ödünç İade İçin Bilgilerin Getirilmesi
        public ActionResult Odunciade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
            return View("Odunciade", odn);
        }

        public ActionResult OduncGuncelle(TBLHAREKET oduncgncl)
        {
            var hrk = db.TBLHAREKET.Find(oduncgncl.ID);
            hrk.UYEGETIRTARIH = oduncgncl.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
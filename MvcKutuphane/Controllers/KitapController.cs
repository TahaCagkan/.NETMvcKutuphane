using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities();//veri tabanına bağlantıyı
        // GET: Kitap
        public ActionResult Index(string arancak)
        {
            //var kitaplar = db.TBLKITAP.ToList();

            //True olanları listele
            var kitaplar = from k in db.TBLKITAP.Where(x => x.DURUM == true) select k;
            //Null veya Boş değilse
            if (!string.IsNullOrEmpty(arancak))
            {
                //arancak değişkeninden aldığımız değeri kitaplar değişkenine at,daha sonra bunu da TBLKITAP içerisinde AD'a göre ara
                kitaplar = kitaplar.Where(m => m.AD.Contains(arancak));
            }
            //içermiyorsa Listeyi dön

            return View(kitaplar.ToList());
            //return View(kitaplar)
        }

        //Kitap Ekleme GET
        [HttpGet]
        public ActionResult KitapEkle()
        {

            //Kategori Adını Listelemek için LINQ Sorgusu 
            List<SelectListItem> kitap = (from t in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = t.AD,
                                               Value = t.ID.ToString()
                                           }).ToList();
            ViewBag.ktp1 = kitap;

            //Yazar Adını Listelemek için LINQ sorgusu 
            List<SelectListItem> kitap2 = (from x in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.AD + ' ' + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.ktp2 = kitap2;

            return View();
        }
        //Kitap Ekle POST
        [HttpPost]
        public ActionResult KitapEkle(TBLKITAP tblktpEkle)
        {

            //Kategori ID yi çektik
            var ktg = db.TBLKATEGORI.Where(k => k.ID == tblktpEkle.TBLKATEGORI.ID).FirstOrDefault();
            //Yazar ID yi çektik
            var yzr = db.TBLYAZAR.Where(y => y.ID == tblktpEkle.TBLYAZAR.ID).FirstOrDefault();

            //atadığımız bilgileri değişkene ata
            tblktpEkle.TBLKATEGORI = ktg;
            tblktpEkle.TBLYAZAR = yzr;
            tblktpEkle.DURUM = true;

            db.TBLKITAP.Add(tblktpEkle); //Kitap Ekle işlemi
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Kitap Silme işlemi
        public ActionResult KitapSil(int id)
        {
            var kitap = db.TBLKITAP.Find(id);
            db.TBLKITAP.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Kitap Güncelleştirmeden önce Bilgileri Getirldi

        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TBLKITAP.Find(id);

            //Kategori Adını Listelemek için LINQ Sorgusu 
            List<SelectListItem> kitap = (from x in db.TBLKATEGORI.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.AD,
                                              Value = x.ID.ToString()
                                          }).ToList();

            ViewBag.ktp1 = kitap;

            //Yazar Adını Listelemek için LINQ sorgusu 
            List<SelectListItem> kitap2 = (from x in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.AD + ' ' + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.ktp2 = kitap2;

            return View("KitapGetir", ktp);
        }

        //Kitap Güncelleştirme İşlemi
        public ActionResult KitapGuncelle(TBLKITAP tblktpGuncelle)
        {
            if (!ModelState.IsValid)
            {
                return View("KitapGetir");
            }
            var kitap = db.TBLKITAP.Find(tblktpGuncelle.ID);
            kitap.AD = tblktpGuncelle.AD;
            kitap.BASIMYIL = tblktpGuncelle.BASIMYIL;
            kitap.SAYFA = tblktpGuncelle.SAYFA;
            kitap.YAYINEVI = tblktpGuncelle.YAYINEVI;
            
            //DropDownList için Kategori ve Yazar ID çektik
            var ktg = db.TBLKATEGORI.Where(k => k.ID == tblktpGuncelle.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID == tblktpGuncelle.TBLYAZAR.ID).FirstOrDefault();
            //Yeni güncellenen bilgileri ID ye göre ata
            kitap.KATEGORIID = ktg.ID;
            kitap.YAZARID = yzr.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
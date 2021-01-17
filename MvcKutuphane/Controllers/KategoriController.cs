using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {

        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities();//Veritabanı bağlantı db adında örnekledik
        // GET: Kategori
        public ActionResult Index()
        {
            var degerler = db.TBLKATEGORI.Where(x => x.DURUM == true).ToList(); //Kategori Sayfasını Listeleme
            return View(degerler);
        }
        //Kategori Ekle GET
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        //Kategori Ekle Post
        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI tblktgrEkle)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }

            db.TBLKATEGORI.Add(tblktgrEkle);//kategori tablomuza ekleme yaptık
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Kategori Sil işlemi
        public ActionResult KategoriSil(int id)
        {
            var kategoriSil = db.TBLKATEGORI.Find(id);//ilk önce id değerine göre bul
            //db.TBLKATEGORI.Remove(kategoriSil);//daha sonra değişken olarka tanımladığımız kategoriSil = id değerine göre sil
            kategoriSil.DURUM = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Güncelle için Bilgileri Aldık
        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);//id yi bul
            return View("KategoriGetir", ktg);
        }
        //Güncelleme işlemi
        public ActionResult KategoriGuncelle(TBLKATEGORI tblktgGuncelle)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriGetir");
            }
            var ktg = db.TBLKATEGORI.Find(tblktgGuncelle.ID); //kategori tablosundaki ID ye göre güncelleyeceğiz.
            ktg.AD = tblktgGuncelle.AD; //Adı Güncelle
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
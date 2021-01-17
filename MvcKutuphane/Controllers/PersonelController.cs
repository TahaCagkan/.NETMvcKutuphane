using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {

        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities(); //Bağlantı
        // GET: Personel
        public ActionResult Index()
        {
            var deger = db.TBLPERSONEL.ToList(); //Personel Tablosunu Listele

            return View(deger);
        }


        //Personel Ekle GET
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        //Personel Ekle Post
        [HttpPost]
        public ActionResult PersonelEkle(TBLPERSONEL tblprEkle)
        {
            if(!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }
            db.TBLPERSONEL.Add(tblprEkle);//Personel tablomuza ekleme yaptık
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Kategori Sil işlemi
        public ActionResult PersonelSil(int id)
        {
            var personelSil = db.TBLPERSONEL.Find(id);//ilk önce id değerine göre bul
            db.TBLPERSONEL.Remove(personelSil);//daha sonra değişken olarka tanımladığımız personelSil = id değerine göre sil
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Güncelle için Bilgileri Aldık
        public ActionResult PersonelGetir(int id)
        {
            var pr = db.TBLPERSONEL.Find(id);//id yi bul
            return View("PersonelGetir", pr);
        }
        //Güncelleme işlemi
        public ActionResult PersonelGuncelle(TBLPERSONEL tblprGuncelle)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelGetir");
            }

            var prs = db.TBLPERSONEL.Find(tblprGuncelle.ID); //personel tablosundaki ID ye göre güncelleyeceğiz.
            prs.PERSONELAD = tblprGuncelle.PERSONELAD; //Adı Güncelle
            prs.PERSONELSOYAD = tblprGuncelle.PERSONELSOYAD; //Adı Güncelle

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class UyelerController : Controller
    {
        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities(); //Bağlantı
        // GET: Uyeler
        public ActionResult Index( string arancak,int sayfa = 1)
        {
            var uyeler = from k in db.TBLUYELER select k;
            //Null veya Boş değilse
            if (!string.IsNullOrEmpty(arancak))
            {
                //arancak değişkeninden aldığımız değeri kitaplar değişkenine at,daha sonra bunu da TBLKITAP içerisinde AD'a göre ara
                uyeler = uyeler.Where(m => m.AD.Contains(arancak));
            }
            //var degerler = db.TBLUYELER.ToList().ToPagedList(sayfa, 5);

            return View(uyeler.ToList());
        }

        //Personel Ekle GET
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        //Personel Ekle Post
        [HttpPost]
        public ActionResult UyeEkle(TBLUYELER tbluyeEkle)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            db.TBLUYELER.Add(tbluyeEkle);//Personel tablomuza ekleme yaptık
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Kategori Sil işlemi
        public ActionResult UyeSil(int id)
        {
            var uyeSil = db.TBLUYELER.Find(id);//ilk önce id değerine göre bul
            db.TBLUYELER.Remove(uyeSil);//daha sonra değişken olarka tanımladığımız personelSil = id değerine göre sil
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Güncelle için Bilgileri Aldık
        public ActionResult UyeGetir(int id)
        {
            var uye = db.TBLUYELER.Find(id);//id yi bul
            return View("UyeGetir", uye);
        }
        //Güncelleme işlemi
        public ActionResult UyeGuncelle(TBLUYELER tbluyeGuncelle)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeGetir");
            }

            var uyes = db.TBLUYELER.Find(tbluyeGuncelle.ID); //personel tablosundaki ID ye göre güncelleyeceğiz.
            uyes.AD = tbluyeGuncelle.AD; //Adı Güncelle
            uyes.SOYAD = tbluyeGuncelle.SOYAD; //Soyadı Güncelle
            uyes.TELEFON = tbluyeGuncelle.TELEFON;
            uyes.ADRES = tbluyeGuncelle.ADRES;
            uyes.MAIL = tbluyeGuncelle.MAIL;
            uyes.FAKULTE = tbluyeGuncelle.FAKULTE;
            uyes.KULLANICIADI = tbluyeGuncelle.KULLANICIADI;
            uyes.SIFRE = tbluyeGuncelle.SIFRE;


            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Üye Kitap Geçmişini Görüntüleme
        public ActionResult UyeKitapGecmis(int id)
        {
            //Hareketler tablsondan üye Id getir
            var ktpgcms = db.TBLHAREKET.Where(x => x.UYEID == id).ToList();
            //Kitap geçmiş sayfasında Kullanıcı Ad ve Soyad Görüntüle
            var uyekit = db.TBLUYELER.Where(x => x.ID == id).Select(y => y.AD + " " + y.SOYAD).FirstOrDefault();
            ViewBag.u1 = uyekit;
            return View(ktpgcms);
        }
    }
}
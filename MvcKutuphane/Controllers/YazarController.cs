using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        DBMvcKutuphaneEntities db = new DBMvcKutuphaneEntities();//veritabanına bağlantıyı örnekledik

        // GET: Yazar
        public ActionResult Index()
        {
            var degerler = db.TBLYAZAR.ToList();//TBLYAZAR içerisindeki bilgileri Listele

            return View(degerler);
        }
        //Yazar Ekle GET
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }

        //YazarEkle Post
        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR tblyazarEkle)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.TBLYAZAR.Add(tblyazarEkle);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        //Yazar Silme işlemi
        public ActionResult YazarSil(int id)
        {
            var yazar = db.TBLYAZAR.Find(id); //silincek yazarın TBLYAZAR dan ID sini bul,değişkene at
            db.TBLYAZAR.Remove(yazar); //bulunan id attığımız değişken aracılığıyla kaldır
            db.SaveChanges(); //durumu kaydet
            return RedirectToAction("Index");
        }

        //Yazar Bilgilerini Güncellemeden Önce Getirme
        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TBLYAZAR.Find(id);

            return View("YazarGetir",yzr);
        }

        //Yazar Bilgilerini Güncelle
        public ActionResult YazarGuncelle(TBLYAZAR tblyazarGuncelle)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarGetir");
            }

            var yzr = db.TBLYAZAR.Find(tblyazarGuncelle.ID);// TBLYAZAR ID yi bul
            yzr.AD = tblyazarGuncelle.AD; //Ad yeni gelen değeri yaz
            yzr.SOYAD = tblyazarGuncelle.SOYAD;//Soyad yeni gelen değeri yaz
            yzr.DETAY = tblyazarGuncelle.DETAY;//Detay yeni gelen değeri yaz
            db.SaveChanges();//Yapılan işlemleri Kaydet
            return RedirectToAction("Index");
        }
        //Yazarın Kitaplarını Gösterme

        public ActionResult YazarKitaplar(int id)
        {
            //Yazar id'yi çekiyor
            var yazar = db.TBLKITAP.Where(x => x.YAZARID == id).ToList();
            //Yazar Ad ve Soyadı al
            var yazarad = db.TBLYAZAR.Where(x => x.ID == id).Select(y => y.AD + "" + y.SOYAD).FirstOrDefault();
            ViewBag.y1 = yazarad;
            return View(yazar);
        }

    }
}
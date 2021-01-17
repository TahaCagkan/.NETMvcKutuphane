using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Models.Siniflarim
{
    //Aynı Sayfada 2 farklı tablo kullanılması gerektiği için IEnumerable dan yararlandık
    public class Listeler
    {
        
        public IEnumerable<TBLKITAP> KitapListesi { get; set; }
        public IEnumerable<TBLHAKKIMIZDA> HakkimizdaListesi { get; set; }
    }
}
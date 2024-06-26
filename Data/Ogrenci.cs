using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data{
    public class Ogrenci //öğrenci entity sınıfı (entity sınıfı => bir veri tabanındaki tablolara karşılık gelir)
    {
        //id => primary key
        [Key]//tanımlanan proportynin key özellikte olduğunu vurgular (id hariç farklı bir isim verilmek istenirse bu attribute kullanılmalıdır)
        public int OgrenciId { get; set; }
        public string? OgrenciAd { get; set; }
        public string? OgrenciSoyad { get; set; }
        public string AdSoyad { 
            get {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            }
        }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();//collection : liste tipinde kurs kayıt alır .(öğrencinin birden fazla kursa kayıt olabileceğinden dolayı)

    }
}
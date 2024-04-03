using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data{
    public class Ogrenci //öğrenci entity sınıfı
    {
        //id => primary key
        [Key]//tanımlanan proportynin key özellikte olduğunu vurgular (id hariç farklı bir isim verilmek istenirse bu attribute kullanılmalıdır)
        public int OgrenciId { get; set; }
        public string? OgrenciAd { get; set; }
        public string? OgrenciSoyad { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

    }
}
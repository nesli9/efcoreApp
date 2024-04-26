using System.ComponentModel.DataAnnotations;
using efcoreApp.Data;

namespace efcoreApp.Models
{
    public class KursViewModel // entity sınıfı
    {
        public int KursId { get; set; }
        [Required]
        [StringLength(50)]//max 50 karakterli bir kurs adı girilmeli
        [Display(Name = "Kurs Başlığı")] //sayfada değişkenin adını nasıl göstermek istiyorsak o şekilde değiştirebiliriz.
        public string? Baslik { get; set; }
        public int OgretmenId { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();


    }
}
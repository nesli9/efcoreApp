using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller{
        private readonly DataContext _context;

        public OgrenciController(DataContext context){
            _context = context;
        }
        public async Task<ActionResult> Index(){
            
            return View(await _context.Ogrenciler.ToListAsync());
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model){ //geri dönüş değeri için task yazılır
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();//async bir metod olduğu için await ile bekletilir.
            return RedirectToAction("Index");
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }

            var ogr = await _context
                                .Ogrenciler
                                .Include(o => o.KursKayitlari)
                                .ThenInclude(o => o.Kurs) //öce modele gidilir daha sonra modelden kurskayıta geçiş yapılıp veri alındığı için thenInclude kullanılır
                                .FirstOrDefaultAsync(o => o.OgrenciId == id);
            // var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciId == id); //bulduğu ilk kaydı dönderir (id ile arananlarda)

            
            if(ogr == null){
                return NotFound();
            }

            return View(ogr);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]// cross-site saldırısını engellemek için güvenlik kontrölü yapar. // formu get ve post eden kişi aynı mı kontrolünü yapar
        public async Task<IActionResult> Edit(int id, Ogrenci model){
            if(id != model.OgrenciId){
                return NotFound();
            }

            if (ModelState.IsValid){
                try{
                    _context.Update(model);
                    await _context.SaveChangesAsync();//veritabanaında güncellemeyi gerçekleştiren satır
                }
                catch (DbUpdateConcurrencyException){
                    if (!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId)){ //kayıt veri tabanında yoksa
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                    
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id){
            if (id == null){
                return NotFound();
            }
            var ogrenci = await _context.Ogrenciler.FindAsync(id);

            if (ogrenci == null){
                return NotFound();
            }

            return View(ogrenci);

        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id) { //formdan gelen idyi alması için fromform binding kullanılır.

            var ogrenci = await _context
                            .Ogrenciler
                            .Include(o => o.KursKayitlari)
                            .ThenInclude(o => o.Kurs)
                            .FirstOrDefaultAsync(o => o.OgrenciId == id);

            if(ogrenci == null){
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        }
    
}

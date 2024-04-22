using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers{
    public class OgretmenController : Controller{
        private readonly DataContext _context;

        public OgretmenController(DataContext context){
            _context = context;
        }
        public async Task<ActionResult> Index(){
            
            return View(await _context.Ogretmenler.ToListAsync());
        }

        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model){ //geri dönüş değeri için task yazılır
            _context.Ogretmenler.Add(model);
            await _context.SaveChangesAsync();//async bir metod olduğu için await ile bekletilir.
            return RedirectToAction("Index");
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }

            var entity = await _context
                                .Ogretmenler
                                .FirstOrDefaultAsync(o => o.OgretmenId == id);
            // var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciId == id); //bulduğu ilk kaydı dönderir (id ile arananlarda)

            
            if(entity == null){
                return NotFound();
            }

            return View(entity);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]// cross-site saldırısını engellemek için güvenlik kontrölü yapar. // formu get ve post eden kişi aynı mı kontrolünü yapar
        public async Task<IActionResult> Edit(int id, Ogretmen model){
            if(id != model.OgretmenId){
                return NotFound();
            }

            if (ModelState.IsValid){
                try{
                    _context.Update(model);
                    await _context.SaveChangesAsync();//veritabanaında güncellemeyi gerçekleştiren satır
                }
                catch (DbUpdateConcurrencyException){
                    if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId)){ //kayıt veri tabanında yoksa
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
        
    }
}
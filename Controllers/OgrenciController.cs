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

        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }

            var ogr = await _context.Ogrenciler.FindAsync(id);
            // var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciId == id); //bulduğu ilk kaydı dönderir (id ile arananlarda)

            
            if(ogr == null){
                return NotFound();
            }

            return View(ogr);

        }
    
    }
}
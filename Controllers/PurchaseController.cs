using MusicShopApp.Data;
using MusicShopApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicShopApp.Controllers
{
    public class PurchaseController(MusicShopAppContext context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var musicShopAppContext = context.Purchase.Include(p => p.Album).Include(p => p.Client);
            return View(await musicShopAppContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await context.Purchase
                .Include(p => p.Album)
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        public IActionResult Create()
        {
            ViewData["AlbumID"] = new SelectList(context.Album, "Id", "Title");
            ViewData["ClientId"] = new SelectList(context.Client, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            var purchase = new Purchase
            {
                ClientId = Convert.ToUInt16(form["ClientId"]),
                AlbumID = Convert.ToUInt16(form["AlbumID"]),
                Date = Convert.ToDateTime(form["Date"])
            };
            
            if (ModelState.IsValid)
            {
                    context.Add(purchase);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }

            ViewData["AlbumID"] = new SelectList(context.Album, "Id", "Title", purchase.AlbumID);
            ViewData["ClientId"] = new SelectList(context.Client, "Id", "FullName", purchase.ClientId);
            
            return View(purchase);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await context.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["AlbumID"] = new SelectList(context.Album, "Id", "Title", purchase.AlbumID);
            ViewData["ClientId"] = new SelectList(context.Client, "Id", "FullName", purchase.ClientId);
            return View(purchase);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
            var purchase = await context.Purchase.FindAsync(id) ?? new Purchase();
            
            if (id != purchase.Id)
            {
                return NotFound();
            }

            purchase.ClientId = Convert.ToUInt16(form["ClientId"]);
            purchase.AlbumID = Convert.ToUInt16(form["AlbumID"]);
            purchase.Date = Convert.ToDateTime(form["Date"]);
            
            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(purchase);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumID"] = new SelectList(context.Album, "Id", "Title", purchase.AlbumID);
            ViewData["ClientId"] = new SelectList(context.Client, "Id", "FullName", purchase.ClientId);
            return View(purchase);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await context.Purchase
                .Include(p => p.Album)
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await context.Purchase.FindAsync(id);
            if (purchase != null)
            {
                context.Purchase.Remove(purchase);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return context.Purchase.Any(e => e.Id == id);
        }
    }
}

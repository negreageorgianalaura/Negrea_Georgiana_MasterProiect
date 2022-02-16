using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Negrea_Georgiana_MasterProiect.Data;
using Negrea_Georgiana_MasterProiect.Models;
using Microsoft.AspNetCore.Authorization;
using Negrea_Georgiana_MasterProiect.Models.ShopViewModels;

namespace Negrea_Georgiana_MasterProiect.Controllers
{
    [Authorize(Policy = "OnlySales")]
    public class SellersController : Controller
    {
        private readonly ShopContext _context;

        public SellersController(ShopContext context)
        {
            _context = context;
        }

        // GET: Sellers
        public async Task<IActionResult> Index(int? id, int? bootID)
        {
            var viewModel = new SellerIndexData();
            viewModel.Sellers = await _context.Sellers
            .Include(i => i.SoldBoots)
            .ThenInclude(i => i.Boot)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.SellerName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["SellerID"] = id.Value;
                Seller seller = viewModel.Sellers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Boots = seller.SoldBoots.Select(s => s.Boot);
            }
            if (bootID != null)
            {
                ViewData["BootID"] = bootID.Value;
                viewModel.Orders = viewModel.Boots.Where(
                x => x.ID == bootID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SellerName,Adress")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var seller = await _context.Sellers
            .Include(i => i.SoldBoots).ThenInclude(i => i.Boot)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }
            PopulateSoldBootData(seller);
            return View(seller);

        }
        private void PopulateSoldBootData(Seller seller)
        {
            var allBoots = _context.Boots;
            var sellerBoots = new HashSet<int>(seller.SoldBoots.Select(c => c.BootID));
            var viewModel = new List<SoldBootData>();
            foreach (var boot in allBoots)
            {
                viewModel.Add(new SoldBootData
                {
                    BootID = boot.ID,
                    Name = boot.Name,
                    IsSold = sellerBoots.Contains(boot.ID)
                });
            }
            ViewData["Boots"] = viewModel;
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedBoots)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sellerToUpdate = await _context.Sellers
            .Include(i => i.SoldBoots)
            .ThenInclude(i => i.Boot)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Seller>(
            sellerToUpdate,
            "",
            i => i.SellerName, i => i.Adress))
            {
                UpdateSoldBoots(selectedBoots, sellerToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateSoldBoots(selectedBoots, sellerToUpdate);
            PopulateSoldBootData(sellerToUpdate);
            return View(sellerToUpdate);
        }
        private void UpdateSoldBoots(string[] selectedBoots, Seller sellerToUpdate)
        {
            if (selectedBoots == null)
            {
                sellerToUpdate.SoldBoots = new List<SoldBoot>();
                return;
            }
            var selectedBootsHS = new HashSet<string>(selectedBoots);
            var soldBoots = new HashSet<int>
            (sellerToUpdate.SoldBoots.Select(c => c.Boot.ID));
            foreach (var boot in _context.Boots)
            {
                if (selectedBootsHS.Contains(boot.ID.ToString()))
                {
                    if (!soldBoots.Contains(boot.ID))
                    {
                        sellerToUpdate.SoldBoots.Add(new SoldBoot
                        {
                            SellerID =
                            sellerToUpdate.ID,
                            BootID = boot.ID
                        });
                    }
                }
                else
                {
                    if (soldBoots.Contains(boot.ID))
                    {
                        SoldBoot bootToRemove = sellerToUpdate.SoldBoots.FirstOrDefault(i
                       => i.BootID == boot.ID);
                        _context.Remove(bootToRemove);
                    }
                }
            }
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.ID == id);
        }
    }
}

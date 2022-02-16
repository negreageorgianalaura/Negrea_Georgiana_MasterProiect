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

namespace Negrea_Georgiana_MasterProiect.Controllers
{
    [Authorize(Roles = "Employee")]

    public class BootsController : Controller
    {
        private readonly ShopContext _context;

        public BootsController(ShopContext context)
        {
            _context = context;
        }

        // GET: Boots
        [AllowAnonymous]
        public async Task<IActionResult> Index(
  string sortOrder,
  string currentFilter,
  string searchString,
  int? pageNumber)

        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var boots = from b in _context.Boots
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                boots = boots.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    boots = boots.OrderByDescending(b => b.Name);
                    break;
                case "Price":
                    boots = boots.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    boots = boots.OrderByDescending(b => b.Price);
                    break;
                default:
                    boots = boots.OrderBy(b => b.Name);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Boot>.CreateAsync(boots.AsNoTracking(), pageNumber ??
           1, pageSize));

        }

        // GET: Boots/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boot = await _context.Boots
 .Include(s => s.Orders)
 .ThenInclude(e => e.Customer)
 .AsNoTracking()
 .FirstOrDefaultAsync(m => m.ID == id);

            if (boot == null)
            {
                return NotFound();
            }

            return View(boot);
        }

        // GET: Boots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Brand,Price")] Boot boot)
        {
            try
            {
                if (ModelState.IsValid)
            {
                _context.Add(boot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }
            return View(boot);
        }

        // GET: Boots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boot = await _context.Boots.FindAsync(id);
            if (boot == null)
            {
                return NotFound();
            }
            ViewData["BootID"] = new SelectList(_context.Boots, "ID", "ID", boot.ID);
            return View(boot);
        }

        // POST: Boots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Brand,Price")] Boot boot)
        {
            if (id != boot.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                    if (!BootExists(boot.ID))
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
            ViewData["BootID"] = new SelectList(_context.Boots, "ID", "ID", boot.ID);
            return View(boot);
        }

        // GET: Boots/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boot = await _context.Boots
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (boot == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(boot);
        }

        // POST: Boots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boot = await _context.Boots.FindAsync(id);
            if (boot == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Boots.Remove(boot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool BootExists(int id)
        {
            return _context.Boots.Any(e => e.ID == id);
        }
    }
}

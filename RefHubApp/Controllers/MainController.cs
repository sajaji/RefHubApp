using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefHubApp.Data;
using RefHubApp.Models;

namespace RefHubApp.Controllers
{
    
    public class MainController : Controller
    {
        private readonly AppDbContext _context;

        public MainController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Main
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Index()
        {
              return _context.mainEntities != null ? 
                          View(await _context.mainEntities.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.mainEntities'  is null.");
        }

        [Authorize(Roles = "Admin, Staff")]
        // GET: Main/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.mainEntities == null)
            {
                return NotFound();
            }

            var mainEntity = await _context.mainEntities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mainEntity == null)
            {
                return NotFound();
            }

            return View(mainEntity);
        }

        [Authorize(Roles = "Admin, Staff")]
        // GET: Main/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Staff")]
        // POST: Main/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,Phone,Lead")] MainEntity mainEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mainEntity);
        }


        [Authorize(Roles = "Admin, Staff")]
        // GET: Main/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.mainEntities == null)
            {
                return NotFound();
            }

            var mainEntity = await _context.mainEntities.FindAsync(id);
            if (mainEntity == null)
            {
                return NotFound();
            }
            return View(mainEntity);
        }

        [Authorize(Roles = "Admin, Staff")]
        // POST: Main/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Email,Phone,Lead")] MainEntity mainEntity)
        {
            if (id != mainEntity.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainEntityExists(mainEntity.ID))
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
            return View(mainEntity);
        }


        [Authorize(Roles = "Admin")]
        // GET: Main/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.mainEntities == null)
            {
                return NotFound();
            }

            var mainEntity = await _context.mainEntities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mainEntity == null)
            {
                return NotFound();
            }

            return View(mainEntity);
        }


        [Authorize(Roles = "Admin")]
        // POST: Main/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.mainEntities == null)
            {
                return Problem("Entity set 'AppDbContext.mainEntities'  is null.");
            }
            var mainEntity = await _context.mainEntities.FindAsync(id);
            if (mainEntity != null)
            {
                _context.mainEntities.Remove(mainEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MainEntityExists(int id)
        {
          return (_context.mainEntities?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

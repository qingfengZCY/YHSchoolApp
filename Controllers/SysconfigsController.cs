using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using YHSchool.Core;
using YHSchool.Data;
using YHSchool.Models;

namespace YHSchool.Controllers
{
    [Authorize]
    public class SysconfigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private IMemoryCache _cache;

        public SysconfigsController(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }

        // GET: Sysconfigs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sysconfigs.ToListAsync());
        }

        // GET: Sysconfigs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sysconfig = await _context.Sysconfigs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sysconfig == null)
            {
                return NotFound();
            }

            return View(sysconfig);
        }

        // GET: Sysconfigs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sysconfigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ConfigKey,ConfigValue,Comments")] Sysconfig sysconfig)
        {
            if (ModelState.IsValid)
            {
                sysconfig.SysType = 1; // Default is WebHook
                _context.Add(sysconfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sysconfig);
        }

        // GET: Sysconfigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sysconfig = await _context.Sysconfigs.SingleOrDefaultAsync(m => m.ID == id);
            if (sysconfig == null)
            {
                return NotFound();
            }
            return View(sysconfig);
        }

        // POST: Sysconfigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ConfigKey,ConfigValue,Comments")] Sysconfig sysconfig)
        {
            if (id != sysconfig.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sysconfig.SysType = 1;
                    _context.Update(sysconfig);
                    await _context.SaveChangesAsync();
                    _cache.Remove(ConstraintStr.CON_DDHookKey);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SysconfigExists(sysconfig.ID))
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
            return View(sysconfig);
        }

        // GET: Sysconfigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sysconfig = await _context.Sysconfigs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sysconfig == null)
            {
                return NotFound();
            }

            return View(sysconfig);
        }

        // POST: Sysconfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sysconfig = await _context.Sysconfigs.SingleOrDefaultAsync(m => m.ID == id);
            _context.Sysconfigs.Remove(sysconfig);
            await _context.SaveChangesAsync();
            _cache.Remove(ConstraintStr.CON_DDHookKey);
            return RedirectToAction(nameof(Index));
        }

        private bool SysconfigExists(int id)
        {
            return _context.Sysconfigs.Any(e => e.ID == id);
        }
    }
}

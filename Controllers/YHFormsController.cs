using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YHSchool.Data;
using YHSchool.Models;

namespace YHSchool.Controllers
{
    public class YHFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YHFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: YHForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.YHForms.ToListAsync());
        }

        // GET: YHForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yHForm = await _context.YHForms
                .SingleOrDefaultAsync(m => m.ID == id);
            if (yHForm == null)
            {
                return NotFound();
            }

            return View(yHForm);
        }

        // GET: YHForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: YHForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FormCode,FormName,MsgTemplate,CreateDate,Creator")] YHForm yHForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yHForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yHForm);
        }

        // GET: YHForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yHForm = await _context.YHForms.SingleOrDefaultAsync(m => m.ID == id);
            if (yHForm == null)
            {
                return NotFound();
            }
            return View(yHForm);
        }

        // POST: YHForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FormCode,FormName,MsgTemplate,CreateDate,Creator")] YHForm yHForm)
        {
            if (id != yHForm.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yHForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YHFormExists(yHForm.ID))
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
            return View(yHForm);
        }

        // GET: YHForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yHForm = await _context.YHForms
                .SingleOrDefaultAsync(m => m.ID == id);
            if (yHForm == null)
            {
                return NotFound();
            }

            return View(yHForm);
        }

        // POST: YHForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yHForm = await _context.YHForms.SingleOrDefaultAsync(m => m.ID == id);
            _context.YHForms.Remove(yHForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YHFormExists(int id)
        {
            return _context.YHForms.Any(e => e.ID == id);
        }
    }
}

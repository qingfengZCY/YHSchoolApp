using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YHSchool.Data;
using YHSchool.Models;

namespace YHSchool.Controllers
{
    [Authorize]
    public class YHFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YHFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: YHForms
        public async Task<IActionResult> Index()
        {
            var yhForms = _context.YHForms
                .Include(c => c.Hook)
                .AsNoTracking();
            return View(await yhForms.ToListAsync());

           // return View(await _context.YHForms.ToListAsync());
        }

        // GET: YHForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var yHForm = await _context.YHForms
               .Include(c => c.Hook)
               .AsNoTracking()
               .SingleOrDefaultAsync(m => m.ID == id);
            if (yHForm == null)
            {
                return NotFound();
            }
            return View(yHForm);

            //var yHForm = await _context.YHForms
            //    .SingleOrDefaultAsync(m => m.ID == id);
            //if (yHForm == null)
            //{
            //    return NotFound();
            //}

            //return View(yHForm);
        }

        
        // GET: YHForms/Create
        public IActionResult Create()
        {
            PopulateWebHookDropDownList();
            return View();
        }

        // POST: YHForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FormCode,FormName,MsgTemplate,CreateDate,Creator,HookID")] YHForm yHForm)
        {
            if (ModelState.IsValid)
            {
                yHForm.Creator = User.Identity.Name;
                yHForm.CreateDate = DateTime.Now;

                yHForm.Modifier = User.Identity.Name;
                yHForm.ModifyDate = DateTime.Now;

                _context.Add(yHForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateWebHookDropDownList(yHForm.Hook);

            return View(yHForm);

        }

        // GET: YHForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.YHForms.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }
            PopulateWebHookDropDownList(course.HookID);

            return View(course);
        }

        // POST: YHForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)  //, [Bind("ID,FormCode,FormName,MsgTemplate,CreateDate,Creator")] YHForm yHForm)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yhformToUpdate = await _context.YHForms.SingleOrDefaultAsync(c => c.ID == id);

           
            if (await TryUpdateModelAsync<YHForm>(yhformToUpdate, "", c => c.HookID, c => c.FormCode, c => c.FormName, c =>c.CreateDate , c =>c.Creator, c => c.MsgTemplate))
            {
                yhformToUpdate.Modifier = User.Identity.Name;
                yhformToUpdate.ModifyDate = DateTime.Now;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    if (!YHFormExists(yhformToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }                    
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateWebHookDropDownList(yhformToUpdate.HookID);
            return View(yhformToUpdate);           
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


        private void PopulateWebHookDropDownList(object selectedHook = null)
        {
            var webHooksQuery = from d in _context.Sysconfigs
                                   where d.SysType==1
                                   orderby d.ConfigKey
                                   select d;
            ViewBag.WebHookList = new SelectList(webHooksQuery.AsNoTracking(), "ID", "ConfigKey", selectedHook);
        }
    }
}

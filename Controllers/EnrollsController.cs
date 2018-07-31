using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGS.CRS.UI.Utility;
using YHSchool.Data;
using YHSchool.Models;

namespace YHSchool.Controllers
{
    [Authorize]
    public class EnrollsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enrolls
        public async Task<IActionResult> Index(EnrollsParam param)
        {
            //return View(await _context.Enrolls.ToListAsync());

            ViewBag.SearchFilters = param.SearchFilters;
            var list = string.IsNullOrWhiteSpace(param.SearchFilters) ?
                 await _context.Enrolls.OrderByDescending(x => x.CreateDate).AsNoTracking().ToListAsync() :
                await _context.Enrolls.Where(x => x.Message.Contains(param.SearchFilters)).OrderByDescending(x => x.CreateDate).AsNoTracking().ToListAsync();

            //paginate
            var list_page = list.Skip(param.Skip).Take(param.PageSize);

            //total count
            var data_count = list.Count;

            param.RequetUrl = Request.QueryString.Value;
            var res = new PagerResult<Enroll>
            {
                Code = 0,
                DataList = list_page,
                Total = data_count,
                PageSize = param.PageSize,
                PageIndex = param.PageIndex,
                RequestUrl = param.RequetUrl
            };
            return View(res);
        }

        // GET: Enrolls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enroll = await _context.Enrolls
                .SingleOrDefaultAsync(m => m.ID == id);
            if (enroll == null)
            {
                return NotFound();
            }

            return View(enroll);
        }

        // GET: Enrolls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enrolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FormCode,Message,OriginMsg,HasSend,CreateDate,Creator")] Enroll enroll)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enroll);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enroll);
        }

        // GET: Enrolls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enroll = await _context.Enrolls.SingleOrDefaultAsync(m => m.ID == id);
            if (enroll == null)
            {
                return NotFound();
            }
            return View(enroll);
        }

        // POST: Enrolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FormCode,Message,OriginMsg,HasSend,CreateDate,Creator")] Enroll enroll)
        {
            if (id != enroll.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enroll);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollExists(enroll.ID))
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
            return View(enroll);
        }

        // GET: Enrolls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enroll = await _context.Enrolls
                .SingleOrDefaultAsync(m => m.ID == id);
            if (enroll == null)
            {
                return NotFound();
            }

            return View(enroll);
        }

        // POST: Enrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enroll = await _context.Enrolls.SingleOrDefaultAsync(m => m.ID == id);
            _context.Enrolls.Remove(enroll);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollExists(int id)
        {
            return _context.Enrolls.Any(e => e.ID == id);
        }
    }
}

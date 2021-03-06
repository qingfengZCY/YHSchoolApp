﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGS.CRS.UI.Utility;
using YHSchool.Data;
using YHSchool.Models;

namespace YHSchool.Controllers
{
    public class EventLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventLogs
        public async Task<IActionResult> Index(EventParams param)
        {
            ViewBag.SearchComments = param.Comments;
            var list = string.IsNullOrWhiteSpace(param.Comments) ?
                 await _context.EventLogs.OrderByDescending(x => x.CreateDate).AsNoTracking().ToListAsync():
                await _context.EventLogs.Where(x => x.Comments.Contains(param.Comments)).OrderByDescending(x => x.CreateDate).AsNoTracking().ToListAsync();

            //paginate
            var list_page = list.Skip(param.Skip).Take(param.PageSize);

            //total count
            var data_count = list.Count;

            param.RequetUrl =  Request.QueryString.Value;
            var res = new PagerResult<EventLog> { Code=0,DataList = list_page,Total = data_count,
                    PageSize = param.PageSize, PageIndex = param.PageIndex, RequestUrl = param.RequetUrl};
            return View(res);
        }

        // GET: EventLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLog = await _context.EventLogs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (eventLog == null)
            {
                return NotFound();
            }

            return View(eventLog);
        }

        // GET: EventLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EnrollID,ActionType,LogLevel,Comments,CreateDate,Creator")] EventLog eventLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventLog);
        }

        // GET: EventLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLog = await _context.EventLogs.SingleOrDefaultAsync(m => m.ID == id);
            if (eventLog == null)
            {
                return NotFound();
            }
            return View(eventLog);
        }

        // POST: EventLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EnrollID,ActionType,LogLevel,Comments,CreateDate,Creator")] EventLog eventLog)
        {
            if (id != eventLog.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventLogExists(eventLog.ID))
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
            return View(eventLog);
        }

        // GET: EventLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLog = await _context.EventLogs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (eventLog == null)
            {
                return NotFound();
            }

            return View(eventLog);
        }

        // POST: EventLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventLog = await _context.EventLogs.SingleOrDefaultAsync(m => m.ID == id);
            _context.EventLogs.Remove(eventLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventLogExists(int id)
        {
            return _context.EventLogs.Any(e => e.ID == id);
        }
    }
}

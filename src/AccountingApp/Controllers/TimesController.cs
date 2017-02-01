using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccountingApp.Data;
using AccountingApp.Models;

namespace AccountingApp.Controllers
{
    /// <summary>
    /// Controller for manage time entries
    /// </summary>
    public class TimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Times
        public async Task<IActionResult> Index(int? customerfilter, int? billedFilter, DateTime? startdatefilter, DateTime? enddatefilter)
        {
            ViewData["DisplayFilter"] = "collapse";
            bool hasFilter = false;

            TimesViewModel model = new TimesViewModel();

            model.times = await _context.Time.OrderBy(m => m.Date).ToListAsync();
            if(customerfilter.HasValue && customerfilter.Value != 0)
            {
                model.times = model.times.Where(m => m.CustomerID == customerfilter).ToList();
                model.customerFilter = customerfilter;
                hasFilter = true;
            }
            if (billedFilter.HasValue && billedFilter.Value != 0)
            {
                bool _billed = (billedFilter == 2);
                model.times = model.times.Where(m => m.Billed == _billed).ToList();
                model.billedFilter = billedFilter;
                hasFilter = true;
            }
            if (startdatefilter.HasValue)
            {
                model.times = model.times.Where(m => m.Date >= startdatefilter.Value).ToList();
                model.startDateFilter = startdatefilter;
                hasFilter = true;
            }
            if (enddatefilter.HasValue)
            {
                model.times = model.times.Where(m => m.Date <= enddatefilter.Value).ToList();
                model.endDateFilter = enddatefilter;
                hasFilter = true;
            }

            if(hasFilter)
            {
                ViewData["DisplayFilter"] = "collapse in";
            }

            model.customers = await _context.Customer.ToListAsync();
            model.customers.Insert(0, new Customer() { ID = 0, Name = "All" });

            model.projects = await _context.Project.ToListAsync();
            return View(model);
        }


        public async Task<IActionResult> Weekly(DateTime? startdatefilter)
        {
            TimesViewModel model = new TimesViewModel();

            DateTime _startWeekFilter = (startdatefilter.HasValue) ? startdatefilter.Value : DateTime.Today.StartOfWeek(DayOfWeek.Monday);
            DateTime _endWeekFilter = _startWeekFilter.AddDays(6);

            model.startDateFilter = _startWeekFilter;
            model.times = await _context.Time.Where(m => m.Date >= _startWeekFilter && m.Date <= _endWeekFilter).OrderBy(m => m.Date).ToListAsync();
            model.customers = await _context.Customer.ToListAsync();
            model.projects = await _context.Project.ToListAsync();
            
            return View(model);
        }


        // GET: Times/Create
        public async Task<IActionResult> Create()
        {
            TimeViewModel model = getTimeViewModel(null);
            return View(model);
        }

        // POST: Times/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Billed,Date,Duration,Memo,ProjectID,CustomerID")] Time time)
        {
            if (ModelState.IsValid)
            {
                _context.Add(time);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            TimeViewModel model = getTimeViewModel(time);
            return View(model);
        }

        // GET: Times/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var time = await _context.Time.SingleOrDefaultAsync(m => m.ID == id);
            if (time == null)
            {
                return NotFound();
            }

            TimeViewModel model = getTimeViewModel(time);
            return View(model);
        }

        // POST: Times/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Billed,Date,Duration,Memo,ProjectID,CustomerID")] Time time)
        {
            if (id != time.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(time);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeExists(time.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            TimeViewModel model = getTimeViewModel(time);
            return View(model);
        }

        // GET: Times/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var time = await _context.Time.SingleOrDefaultAsync(m => m.ID == id);
            if (time == null)
            {
                return NotFound();
            }

            TimeViewModel model = getTimeViewModel(time);
            return View(model);
        }

        // POST: Times/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var time = await _context.Time.SingleOrDefaultAsync(m => m.ID == id);
            _context.Time.Remove(time);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TimeExists(int id)
        {
            return _context.Time.Any(e => e.ID == id);
        }

        private TimeViewModel getTimeViewModel(Time time)
        {
            TimeViewModel timeViewModel = new TimeViewModel();
            timeViewModel.time = time;
            timeViewModel.customers = _context.Customer.ToList();
            timeViewModel.projects = _context.Project.ToList();
            return timeViewModel;
        }

    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
    }
}

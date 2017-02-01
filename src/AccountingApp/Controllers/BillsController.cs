using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccountingApp.Data;
using AccountingApp.Models;
using Microsoft.Extensions.Options;

namespace AccountingApp.Controllers
{
    /// <summary>
    /// Controller for manage inovices
    /// </summary>
    public class BillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            BillsViewModel model = new BillsViewModel();
            model.bills = await _context.Bill.OrderBy(m => m.Date).ToListAsync();
            model.customers = await _context.Customer.ToListAsync();
            return View(model);
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill.SingleOrDefaultAsync(m => m.ID == id);
            if (bill == null)
            {
                return NotFound();
            }

            BillViewModel model = new BillViewModel();
            model.customers = _context.Customer.ToList();
            model.projects = _context.Project.ToList();
            model.bill = bill;
            List<string> times = model.bill.Times.Split(',').ToList();
           
            model.times = _context.Time.Where(timeItem => times.Any(x => timeItem.ID.ToString().Contains(x))).ToList();
            
            return View(model);
        }

        // GET: Bills/Create
        public IActionResult Create(int? customer)
        {
            BillViewModel model = new BillViewModel();
            model.customers = _context.Customer.ToList();
            
            if(customer.HasValue) { 
                model.customerSelected = _context.Customer.SingleOrDefault(m => m.ID == customer);

                int _number = 1;
                if (_context.Bill.Count() > 0)
                {
                    _number = _context.Bill.Max(m => m.Number) + 1;
                }
                model.bill.Number = _number;

                model.times = _context.Time.Where(m => m.CustomerID == customer && !m.Billed).ToList();

                model.projects = _context.Project.ToList();
            }

            return View(model);
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int customerSelected_ID, int number, int amount, DateTime date, string times)
        {
            //[Bind("ID,Amount,CustomerID,Date,Number")] Bill bill

            Bill bill = new Bill();
            bill.CustomerID = customerSelected_ID;
            bill.Amount = amount;
            bill.Number = number;
            bill.Date = date;
            bill.Times = times;
            
            _context.Add(bill);
            await _context.SaveChangesAsync();

            ///set billed the selected times
            string[] _timesID = times.Split(',');
            foreach(string timeID in _timesID)
            {
                var time = _context.Time.SingleOrDefault(m => m.ID == int.Parse(timeID));
                time.Billed = true;
                _context.Update(time);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill.SingleOrDefaultAsync(m => m.ID == id);
            if (bill == null)
            {
                return NotFound();
            }

            BillViewModel model = new BillViewModel();
            model.customers = _context.Customer.ToList();
            model.bill = bill;

            return View(model);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bill.SingleOrDefaultAsync(m => m.ID == id);
            string times = bill.Times;
            string[] _timesID = times.Split(',');

            _context.Bill.Remove(bill);
            await _context.SaveChangesAsync();

            ///remove billed from the associated times
            
            foreach (string timeID in _timesID)
            {
                var time = _context.Time.SingleOrDefault(m => m.ID == int.Parse(timeID));
                time.Billed = false;
                _context.Update(time);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.ID == id);
        }
    }
}

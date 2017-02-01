using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AccountingApp.Models
{
    /// <summary>
    /// invoices model for the list view
    /// </summary>
    public class BillsViewModel
    {
        [Description("list of invoices")]
        public List<Bill> bills;

        [Description("list of customers")]
        public List<Customer> customers;
    }
}

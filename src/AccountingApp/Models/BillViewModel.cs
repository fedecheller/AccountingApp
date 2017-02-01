using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AccountingApp.Models
{
    /// <summary>
    /// invoice model for the single element view
    /// </summary>
    public class BillViewModel
    {
        [Description("the invoice")]
        public Bill bill;

        [Description("list of selectable customers")]
        public List<Customer> customers;

        [Description("list of selectable projects")]
        public List<Project> projects;

        [Description("the customer selected for the new invoice")]
        public Customer customerSelected;

        [Description("list of times selected in the invoice")]
        public List<Time> times;

        [Description("work hour price")]
        public int hourPrice = 50;  //need to move in appsettings.json

        public BillViewModel()
        {
            bill = new Bill() { Date = DateTime.Today };
        }
    }
}

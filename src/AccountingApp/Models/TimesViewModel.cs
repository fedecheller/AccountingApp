using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingApp.Models
{
    /// <summary>
    /// time entries model for the list view
    /// </summary>
    public class TimesViewModel
    {
        [Description("time entries for the view")]
        public List<Time> times;

        [Description("customers list for the view")]
        public List<Customer> customers;

        [Description("projects list for the view")]
        public List<Project> projects;


        [Description("customer filter for time entries")]
        public int? customerFilter;

        [Description("is billed filter for time entries")]
        public int? billedFilter;

        [Description("start date filter for time entries")]
        public DateTime? startDateFilter;

        [Description("end date filter for time entries")]
        public DateTime? endDateFilter;

    }
}

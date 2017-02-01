using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingApp.Models
{
    /// <summary>
    /// time entry model for the single element view
    /// </summary>
    public class TimeViewModel
    {
        [Description("time entry for the view")]
        public Time time;

        [Description("customers list for the view")]
        public List<Customer> customers;

        [Description ("projects list for the view")]
        public List<Project> projects;
    }
}

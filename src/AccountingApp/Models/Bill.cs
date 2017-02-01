using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingApp.Models
{
    /// <summary>
    /// invoice model
    /// </summary>
    public class Bill
    {
        [Description("database identifier")]
        public int ID { get; set; }

        [Display(Name = "Invoice No")]
        [Description("")]
        public int Number { get; set; }

        [DataType(DataType.Date)]
        [Description("")]
        public DateTime Date { get; set; }

        [Description("")]
        public int Amount { get; set; }

        [Display(Name = "Customer")]
        [Description("")]
        public int CustomerID { get; set; }

        [Description("")]
        public string Times { get; set; }
    }
}

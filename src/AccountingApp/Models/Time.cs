using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Models
{
    /// <summary>
    /// time entry model
    /// </summary>
    public class Time
    {
        [Description("database identifier")]
        public int ID { get; set; }

        [StringLength(160)]
        [Description("time description")]
        public string Memo { get; set; }

        [Display(Name = "Customer")]
        [Description("customer identifier")]
        public int CustomerID { get; set; }

        [Display(Name = "Project")]
        [Description("project identifier")]
        public int ProjectID { get; set; }

        [DataType(DataType.Date)]
        [Description("day of time entry")]
        public DateTime Date { get; set; }

        [Range(1, 24, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Description("hour of time entry")]
        public int Duration { get; set; }

        [Description("if the time is been invoiced")]
        public bool Billed { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AccountingApp.Models
{
    /// <summary>
    /// customer model
    /// </summary>
    public class Customer
    {
        [Description("database identifier")]
        public int ID { get; set; }

        [StringLength(120)]
        [Description("customer name")]
        public string Name { get; set; }
    }
}

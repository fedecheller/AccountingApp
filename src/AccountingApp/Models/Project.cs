using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AccountingApp.Models
{
    /// <summary>
    /// project model
    /// </summary>
    public class Project
    {
        [Description("database identifier")]
        public int ID { get; set; }

        [StringLength(120)]
        [Description("project name")]
        public string Name { get; set; }
    }
}

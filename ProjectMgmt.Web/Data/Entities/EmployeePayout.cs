using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMgmt.Web.Data.Entities
{
    public class EmployeePayout : BaseEntity<int>
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Notes { get; set; }

        public Project Project { get; set; }

        public Employee Employee { get; set; }
    }
}

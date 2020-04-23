using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Models
{
    public class EmployeePayoutDetailsViewModel
    {
        public int Id { get; set; }

        public Guid ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        public double Amount { get; set; }

        [Display(Name = "Payment Date")]
        public string PaymentDateStr { get; set; }

        public string Notes { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}

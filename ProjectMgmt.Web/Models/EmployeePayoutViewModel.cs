using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Models
{
    public class EmployeePayoutViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        public double Amount { get; set; }

        [Display(Name = "Payment Date")]
        public string PaymentDateStr { get; set; }
    }
}

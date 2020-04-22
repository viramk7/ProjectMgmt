using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Models
{
    public class CreateIncomeViewModel
    {
        public CreateIncomeViewModel()
        {

        }

        public CreateIncomeViewModel(Guid projectId)
        {
            ProjectId = projectId;
            IncomeDate = DateTime.Now;
        }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [MaxLength(250)]
        public string IncomeFrom { get; set; }

        [Required]
        [Range(1, 100000)]
        public double Amount { get; set; }

        [Required]
        public DateTime IncomeDate { get; set; }
    }
}

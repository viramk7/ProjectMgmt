using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Models
{
    public class EditIncomeViewModel
    {
        [Required]
        public int Id { get; set; }

        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        [Required]
        [MaxLength(250)]
        public string IncomeFrom { get; set; }

        [Required]
        [Range(1,100000)]
        public double Amount { get; set; }

        [Required]
        public DateTime IncomeDate { get; set; }
    }
}

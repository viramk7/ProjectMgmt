using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Data.Entities
{
    public class ProjectIncome : IEntity<int>
    {
        public int Id { get ; set ; }

        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        [MaxLength(250)]
        public string IncomeFrom { get; set; }

        public double Amount { get; set; }

        public DateTime IncomeDate { get; set; }

        public Guid CreatedBy { get ; set ; }
        public DateTime CreatedOn { get ; set ; }
        public Guid? UpdatedBy { get ; set ; }
        public DateTime? UpdatedOn { get ; set ; }
    }
}

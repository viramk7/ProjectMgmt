using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Data.Entities
{
    public class Project : IEntity<Guid>
    {
        public Project()
        {
            Incomes = new List<ProjectIncome>();
            ProjectEmployees = new List<ProjectEmployee>();
            EmployeePayouts = new List<EmployeePayout>();
        }

        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public List<ProjectIncome> Incomes { get; set; }

        public List<ProjectEmployee> ProjectEmployees { get; set; }

        public List<EmployeePayout> EmployeePayouts { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

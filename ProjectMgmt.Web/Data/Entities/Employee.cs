using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Data.Entities
{
    public class Employee : IEntity<int>
    {
        public Employee()
        {
            ProjectEmployees = new List<ProjectEmployee>();
        }

        public int Id { get ; set ; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        public Guid CreatedBy { get ; set ; }
        
        [Required]
        public DateTime CreatedOn { get ; set ; }
        
        public Guid? UpdatedBy { get ; set ; }
        
        public DateTime? UpdatedOn { get ; set ; }

        public List<ProjectEmployee> ProjectEmployees { get; set; }
    }
}

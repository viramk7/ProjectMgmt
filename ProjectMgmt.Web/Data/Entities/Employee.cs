using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Data.Entities
{
    public class Employee : IEntity<int>
    {
        public int Id { get ; set ; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public Guid CreatedBy { get ; set ; }
        
        [Required]
        public DateTime CreatedOn { get ; set ; }
        
        public Guid? UpdatedBy { get ; set ; }
        
        public DateTime? UpdatedOn { get ; set ; }
    }
}

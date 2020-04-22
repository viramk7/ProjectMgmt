using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Data.Entities
{
    public class Project : IEntity<Guid>
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

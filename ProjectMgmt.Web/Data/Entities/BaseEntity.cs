using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMgmt.Web.Data.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }
        
        public DateTime? UpdatedOn { get; set; }
    }
}

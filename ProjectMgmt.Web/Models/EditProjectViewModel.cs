using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Models
{
    public class EditProjectViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}

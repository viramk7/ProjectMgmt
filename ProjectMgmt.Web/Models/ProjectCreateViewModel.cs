using System.ComponentModel.DataAnnotations;

namespace ProjectMgmt.Web.Models
{
    public class ProjectCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}

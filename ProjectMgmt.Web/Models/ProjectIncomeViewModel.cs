using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMgmt.Web.Models
{
    public class ProjectIncomeViewModel
    {
        public int Id { get; set; }

        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string IncomeFrom { get; set; }

        public double Amount { get; set; }

        public DateTime IncomeDate { get; set; }
    }
}

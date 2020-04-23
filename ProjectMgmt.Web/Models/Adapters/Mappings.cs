using AutoMapper;
using ProjectMgmt.Web.Data.Entities;
using System.Globalization;

namespace ProjectMgmt.Web.Models.Adapters
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Project, ProjectViewModel>();
            CreateMap<ProjectCreateViewModel, Project>();
            CreateMap<Project, EditProjectViewModel>().ReverseMap();
            CreateMap<Project, ProjectDetailsViewModel>();

            // Project Income
            CreateMap<ProjectIncome, ProjectIncomeViewModel>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));
            CreateMap<CreateIncomeViewModel, ProjectIncome>();
            CreateMap<ProjectIncome, EditIncomeViewModel>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Project.Id)) 
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));
            CreateMap<EditIncomeViewModel, ProjectIncome>();
            CreateMap<ProjectIncome, IncomeDetailsViewModel>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));

            //Emp Payouts
            CreateMap<EmployeePayout, EmployeePayoutDetailsViewModel>()
                .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Project.Name))
                .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.Name))
                .ForMember(d => d.PaymentDateStr, opt => opt.MapFrom(s => s.PaymentDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
        }
    }
}

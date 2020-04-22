using AutoMapper;
using ProjectMgmt.Web.Data.Entities;

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
        }
    }
}

using AutoMapper;
using Project_management_system.CQRS.Projects.Queries;
using Project_management_system.Models;

namespace Project_management_system.Profiles
{
    public class ProjectProfile:Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectListDTO>().ReverseMap();
        }
    }
}

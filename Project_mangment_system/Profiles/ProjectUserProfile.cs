using AutoMapper;
using Project_management_system.CQRS.ProjectUsers.Commands;
using Project_management_system.Models;

namespace Project_management_system.Profiles
{
    public class ProjectUserProfile:Profile
    {
        public ProjectUserProfile()
        {
            CreateMap<AddUserToProjectCommand, ProjectsUsers>();
        }
    }
}

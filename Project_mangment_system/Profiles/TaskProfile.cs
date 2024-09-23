using AutoMapper;
using MailKit.Security;
using Project_management_system.CQRS.Tasks.Commands;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.Models;
using Project_management_system.ViewModels.Task;

namespace Project_management_system.Profiles
{
    public class ProjectTaskProfile : Profile
    {
        public ProjectTaskProfile()
        {
            CreateMap<ProjectTask, ProjectTaskDto>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ReverseMap();
            CreateMap<ProjectTaskDto, ProjectTaskVM>().ReverseMap();
            CreateMap<AddTaskCommand, ProjectTask>();
            CreateMap<AddTaskVM,AddTaskCommand>().ForMember(dst=>dst.CreatedOn,opt=>opt.MapFrom(src=>DateTime.Now));
            CreateMap<AddUserTaskVM, AddTaskToUserCommand>();
        }
    }
}

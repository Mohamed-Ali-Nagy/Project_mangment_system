using AutoMapper;
using MailKit.Security;
using Project_management_system.CQRS.Tasks.Commands;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.Models;
using Project_management_system.ViewModels.Task;
using Project_management_system.ViewModels.TaskVMs;

namespace Project_management_system.Profiles
{
    public class ProjectTaskProfile : Profile
    {
        public ProjectTaskProfile()
        {
            CreateMap<ProjectTask, ProjectTaskDTO>()

                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ReverseMap();
            CreateMap<ProjectTask, ProjectTaskDto>()

              .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.Name))
              .ReverseMap();
            //ProjectTaskDto
            CreateMap<ProjectTaskDto, ProjectTaskVM>().ReverseMap();
            CreateMap<AddTaskCommand, ProjectTask>();
            CreateMap<AddTaskVM,AddTaskCommand>().ForMember(dst=>dst.CreatedOn,opt=>opt.MapFrom(src=>DateTime.Now));
            CreateMap<AddUserTaskVM, AddTaskToUserCommand>();
            CreateMap<ProjectTaskDTO, ProjectTaskVM>().ReverseMap();
    
            CreateMap<IGrouping<Enums.TaskStatus, ProjectTask>, TaskByStatusDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Key.ToString()))
                .ForMember(dest => dest.TaskDTO, opt => opt.MapFrom(src => src.ToList()));
            //
            CreateMap<TaskByStatusDTO, TaskByStatusVM>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.TaskVMs, opt => opt.MapFrom(src=>src.TaskDTO));
        }
    }
}

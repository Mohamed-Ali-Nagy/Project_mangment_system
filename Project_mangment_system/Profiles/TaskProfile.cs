using AutoMapper;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.ViewModels.TaskVMs;

namespace Project_management_system.Profiles
{
    public class TaskProfile:Profile
    {
        public TaskProfile()
        {
            CreateMap<Models.Task, TaskDTO>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.Name));
           CreateMap<TaskDTO, TaskVM>();
            CreateMap<IGrouping<Enums.TaskStatus, Models.Task>, TaskByStatusDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Key.ToString()))
                .ForMember(dest => dest.TaskDTO, opt => opt.MapFrom(src => src.ToList()));
            //
            CreateMap<TaskByStatusDTO, TaskByStatusVM>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.TaskVMs, opt => opt.MapFrom(src=>src.TaskDTO));
        }
    }
}

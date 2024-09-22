using AutoMapper;
using Project_management_system.CQRS.Task.Command;
using Project_management_system.CQRS.Task.Query;
using Project_management_system.Models;
using Project_management_system.ViewModels.TaskVM;

namespace Project_management_system.Profiles
{
	public class TaskProfile:Profile
	{
        public TaskProfile()
        {
			CreateMap<AddTaskCommand, Models.Task>()
			   .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));


			CreateMap<AddTaskViemModel, AddTaskCommand>();
			

			CreateMap<Models.Task, TaskToReturnDto>()
			   .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User != null ? src.User.Name : "Unassigned"))
			   .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project.Title))
			   .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
			   .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.CreatedOn));
		}
    }
}

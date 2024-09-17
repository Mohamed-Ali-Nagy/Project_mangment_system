using AutoMapper;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.Models;
using Project_management_system.DTO.UserDTOs;
using Project_management_system.Models;
using Project_management_system.ViewModels.UserVMs;

namespace Project_management_system.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserLoginVM, UserLoginDTO>();
            CreateMap<ResetPasswordVM, ResetPasswordCommand>().ReverseMap();
            CreateMap<UserLoginDTO, User>();
            CreateMap<User, UserDetailsDTO>()
                .ForMember(dst=>dst.Roles,
                opt=>opt.MapFrom(src=>src.UserRoles.Select(x=>x.Role.ToString())));
        }
    }
}

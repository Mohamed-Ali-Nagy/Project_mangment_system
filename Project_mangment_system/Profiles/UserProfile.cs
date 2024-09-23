using AutoMapper;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.CQRS.Users.Queries;
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
            CreateMap<UserLoginVM, UserLoginCommand>();
            CreateMap<ResetPasswordVM, ResetPasswordCommand>().ReverseMap();
            CreateMap<VerifyEmailVM, VerifyOTPCommand>().ReverseMap();
            CreateMap<UserRegisterCommand,User>();
            CreateMap<UserRegisterVM,UserRegisterCommand>();
            CreateMap<User,UserListDTO>();
            CreateMap<UserRegisterCommand, User>();
            CreateMap<UserRegisterVM, UserRegisterCommand>();
            CreateMap<ChangePasswordVM, ChangePasswordCommand>();
        }
    }
}

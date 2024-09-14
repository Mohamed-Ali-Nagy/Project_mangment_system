using AutoMapper;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.ViewModels.UserVMs;

namespace Project_management_system.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserLoginVM, UserLoginDTO>();
        }
    }
}

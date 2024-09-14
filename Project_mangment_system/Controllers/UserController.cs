using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.Helpers;
using Project_management_system.ViewModels;
using Project_management_system.ViewModels.UserVMs;

namespace Project_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        IMapper _mapper;
        public UserController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task <ResultVM<string>> UserLogin(UserLoginVM viewModel)
        {
            var userDTO = MapperHelper.MapOne<UserLoginDTO>(viewModel);
           var data = await _mediator.Send(new UserLoginCommand(userDTO));
            return  ResultVM<string>.Sucess(data);
           // return result;
        }
    }
}

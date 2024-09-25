using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.DTO;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Commands
{
    public record UpdateUserCommand(int id,string Name, string PhoneNumber, string ImageURL, string Country):IRequest<ResultDTO<bool>>;
    public record UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResultDTO<bool>>
    {
        private readonly IBaseRepository<User> _repository; 
        private readonly IMediator _mediator;
        public UpdateUserHandler(IBaseRepository<User> repository, IMediator mediator)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResultDTO<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result =await _mediator.Send(new GetUserByIdQuery(request.id));
            if (!result.IsSuccess)
            {
                return ResultDTO<bool>.Faliure("Can not find user with this id");
            }
            var user = result.Data;
              user.Name=request.Name;
            user.PhoneNumber=request.PhoneNumber;
            user.ImageURL=request.ImageURL;
            user.Country=request.Country;

            _repository.Update(user);
            _repository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);

        }
    }
}

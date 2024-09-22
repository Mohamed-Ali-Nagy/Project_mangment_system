using MediatR;
using Project_management_system.CQRS.User.Commands.Orchestrators;
using Project_management_system.CQRS.User.Queries;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.Helpers;
using Project_management_system.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Project_management_system.CQRS.User.Commands
{
	public class RegiterAccountCommand:IRequest<RegisterAccountToReturnDTo>
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Country { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
	   ErrorMessage = "Password must be at least 8 characters long, and include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
		public string Password { get; set; }
	}

	public class RegisterAccountToReturnDTo
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public bool IsSuccessed { get; set; } = true;
		public string ErrorMessage { get; set; }
	}

	public class RegisterAccountCommandHandler : IRequestHandler<RegiterAccountCommand, RegisterAccountToReturnDTo>
	{
		private readonly IMediator _mediator;
		private readonly IBaseRepository<Models.User> _repository;

		public RegisterAccountCommandHandler(IMediator mediator, IBaseRepository<Models.User> repository)
        {
			_mediator = mediator;
			_repository = repository;
		}
        public async Task<RegisterAccountToReturnDTo> Handle(RegiterAccountCommand request, CancellationToken cancellationToken)
		{
			var userExists = await _mediator.Send(new CheckUserExistQuery
			{
				Email = request.Email,
				UserName = request.UserName
			});
            if (userExists)
            {
				return new RegisterAccountToReturnDTo
				{
					IsSuccessed = true,
					ErrorMessage = "A user with this email or username already exists"
				};
            }

			var user = new Models.User
			{
				Name = request.UserName,
				Email = request.Email,
				Country = request.Country,
				PhoneNumber = request.PhoneNumber,
				Password = request.Password,
				IsVerified = false,

			};

			await _repository.AddAsync(user);
			await _repository.SaveChangesAsync();
			var registerAccouctToReturnDto = user.Map<RegisterAccountToReturnDTo>();
			return registerAccouctToReturnDto;
        }
	}


}

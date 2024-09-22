using MediatR;
using Project_management_system.CQRS.Users.Queries;

namespace Project_management_system.CQRS.User.Commands.Orchestrators
{
	public class RegitserAccountOrchestrator:IRequest<RegisterAccountOrchestratorToReturnDto>
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Country { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }

	}
	public class RegisterAccountOrchestratorToReturnDto
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public bool IsSuccess { get; set; } = true;
		public string ErrorMessage { get; set; }
	}
	public class RegisterAccountOrchestratorHandler : IRequestHandler<RegitserAccountOrchestrator, RegisterAccountOrchestratorToReturnDto>
	{
		private readonly IMediator _mediator;

		public RegisterAccountOrchestratorHandler(IMediator mediator)
        {
			_mediator = mediator;
		}
        public  async Task<RegisterAccountOrchestratorToReturnDto> Handle(RegitserAccountOrchestrator request, CancellationToken cancellationToken)
		{

			var registerAccountResult = await _mediator.Send(new RegiterAccountCommand
			{
				UserName = request.UserName,
				Email = request.Email,
				Country = request.Country,
				PhoneNumber = request.PhoneNumber,
				Password = request.Password
			});
			if (registerAccountResult.IsSuccessed)
			{
				return new RegisterAccountOrchestratorToReturnDto
				{
					IsSuccess = false,
					ErrorMessage = registerAccountResult.ErrorMessage
				};
			}

			var user = await _mediator.Send(new GetUserByEmailQuery(request.Email));

			if (user == null)
			{
				return new RegisterAccountOrchestratorToReturnDto
				{
					IsSuccess = false,
					ErrorMessage = "User not found "
				};

			}
			// verfiy Email
			return new RegisterAccountOrchestratorToReturnDto
			{
				Id = registerAccountResult.Id,
				Email = registerAccountResult.Email,
			};
		}
	}
}

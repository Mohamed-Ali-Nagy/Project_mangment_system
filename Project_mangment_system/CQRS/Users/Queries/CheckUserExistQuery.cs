using MediatR;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.User.Queries
{
	public class CheckUserExistQuery:IRequest<bool>
	{
		public string UserName { get; set; }
		public string Email { get; set; }

	}
	public class CheckUserExistsQueryHandler : IRequestHandler<CheckUserExistQuery, bool>
	{
		private readonly IBaseRepository<Models.User> _repository;

		public CheckUserExistsQueryHandler(IBaseRepository<Models.User> repository)
        {
			_repository = repository;
		}
        public async Task<bool> Handle(CheckUserExistQuery request, CancellationToken cancellationToken)
		{
			var existingUser = await _repository.GetAsyncWithSpec(u => u.Email == request.Email || u.Name == request.UserName);
			return existingUser.Any();
		}
	}
}

using MediatR;
using Project_management_system.CQRS.Projects.Queries;
using Project_management_system.DTO;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;
using Project_management_system.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Project_management_system.CQRS.Users.Queries
{
    public record GetAllUsersQuery(int pageNumber,int pageSize):IRequest<PaginatedList<UserListDTO>>
    {

    }
    public record UserListDTO
    {
        public string Name { get; set; }
    
        public string Email { get; set; }
           
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
     
    }

    public record GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserListDTO>>
    {
        private readonly IBaseRepository<User> _repository;
        public GetAllUsersHandler(IBaseRepository<User> baseRepository)
        {
            _repository = baseRepository;
        }
        public async Task<PaginatedList<UserListDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().Map<UserListDTO>();
            var userPaginatedList = await PaginatedList<UserListDTO>.CreateAsync(query, request.pageNumber, request.pageSize);
            return userPaginatedList;

        }
    }
}

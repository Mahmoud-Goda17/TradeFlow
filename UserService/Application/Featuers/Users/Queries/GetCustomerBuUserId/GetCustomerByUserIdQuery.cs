using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetCustomerBuUserId
{
    public sealed record GetCustomerByUserIdQuery(Guid userid) : IRequest<Result<CustomerDto>>
    {
    }
}

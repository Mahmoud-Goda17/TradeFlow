using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetCustomerById
{
    public sealed record GetCutomerByIdQuery(Guid Id) : IRequest<Result<CustomerDto>>
    {
    }
}

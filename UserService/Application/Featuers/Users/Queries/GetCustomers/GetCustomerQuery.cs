using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetCustomers
{
    public sealed record  GetCustomerQuery: IRequest<Result<List<CustomerDto>>>
    {
    }
}

using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Command.CreateCustomer
{
    public sealed record class CreateCustomerCommand(Guid usreid, string companyName, string taxNumber): IRequest<Result<CustomerDto>>
    {
    }
}

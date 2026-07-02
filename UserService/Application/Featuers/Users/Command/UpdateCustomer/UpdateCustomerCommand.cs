using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.UpdateCustomer
{
    public sealed record class UpdateCustomerCommand(Guid usreid, string companyName, string taxNumber) : IRequest<Result<Updated>>
    {
    }
}

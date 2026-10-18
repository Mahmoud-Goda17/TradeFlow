using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.RemoveCustomer
{
    public sealed record RemoveCustomerCommand(Guid Id) : IRequest<Result<Deleted>>
    {
    }
}

using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.RemoveSupplier
{
    public sealed record class RemoveSupplierCommand(Guid Id) : IRequest<Result<Deleted>>
    { 
    }
}

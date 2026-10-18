using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.RemoveSupplier
{
    public sealed record  RemoveSupplierCommand(Guid Id) : IRequest<Result<Deleted>>
    { 
    }
}

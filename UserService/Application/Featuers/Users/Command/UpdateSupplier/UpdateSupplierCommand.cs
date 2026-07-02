using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.UpdateSupplier
{
    public sealed record class UpdateSupplierCommand(Guid userid,
        string companyName,
        string taxNumber,
        string bankAccount) : IRequest<Result<Updated>>
    {
    }
}

using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Command.CreateSupplier
{
    public sealed record class CreateSupplierCommand(Guid userid,
        string companyName,
        string taxNumber,
        string bankAccount) : IRequest<Result<SupplierDto>>
    {
    }
}

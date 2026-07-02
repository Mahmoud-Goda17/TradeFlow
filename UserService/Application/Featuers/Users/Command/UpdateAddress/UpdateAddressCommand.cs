using MediatR;
using Shared.Results;
using UserService.Domain.Addresses;

namespace UserService.Application.Featuers.Users.Command.UpdateAddress
{
    public sealed record class UpdateAddressCommand(Guid userid,List<AddressData> Addresses) : IRequest<Result<Updated>>
    {
    }
}

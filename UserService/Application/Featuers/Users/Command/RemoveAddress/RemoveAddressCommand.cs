using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.RemoveAddress
{
    public sealed record class RemoveAddressCommand(Guid userid):IRequest<Result<Deleted>>
    {
    }
}

using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.RemoveAddress
{
    public sealed record  RemoveAddressCommand(Guid userid):IRequest<Result<Deleted>>
    {
    }
}

using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Command.CreateAddress
{
    public sealed record  CreateAddressCommand (Guid userid,string city,string street,string buildingNo,string? postalCode) : IRequest<Result<AddressDto>>
    {
    }
}

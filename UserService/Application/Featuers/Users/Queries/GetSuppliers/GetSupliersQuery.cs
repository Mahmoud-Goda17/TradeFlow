using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetSuppliers
{
    public sealed record class GetSupliersQuery : IRequest<Result<List<SupplierDto>>>
    {
    }
}

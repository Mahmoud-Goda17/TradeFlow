using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetSupplierByCompanyName
{
    public sealed record  GetSupplierByCompanyNameQuery(string companyname):IRequest<Result<List<SupplierDto>>>
    {
    }
}

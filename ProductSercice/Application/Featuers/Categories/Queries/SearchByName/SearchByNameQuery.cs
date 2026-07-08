using MediatR;
using ProductSercice.Application.Featuers.Categories.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Queries.SearchByName
{
    public sealed record class SearchByNameQuery(string name) : IRequest<Result<List<CategoryDto>>>
    {
    }
}

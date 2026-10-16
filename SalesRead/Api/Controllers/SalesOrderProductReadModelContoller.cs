using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesRead.Application.SalesOrderProductReadModels.Queires.GetSalesOrderProductReadModel;

namespace SalesRead.Api.Controllers
{
    [Route("api/[controller]")]

    public class SalesOrderProductReadModelContoller(ISender sender) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetSalesOrderProductReadModelQuery(), ct);
            if (result.IsSuccess)
                return Ok(result);
            return Problem(result.Errors);
        }
    }
}

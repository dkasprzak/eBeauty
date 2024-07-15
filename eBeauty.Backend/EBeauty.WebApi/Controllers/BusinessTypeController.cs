using EBeauty.Application.Logic.BusinessTypeFunctions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBeauty.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BusinessTypeController : BaseController
{
    public BusinessTypeController(ILogger<BusinessTypeController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetBusinessTypeList()
    {
        var result = await _mediator.Send(new BusinessTypeListQuery.Request());
        return Ok(result);
    }
}

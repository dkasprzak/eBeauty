using EBeauty.Application.Logic.BusinessFunctions.Commands;
using EBeauty.Application.Logic.BusinessFunctions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBeauty.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BusinessController : BaseController
{
    public BusinessController(ILogger<BusinessController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetBusinessDetail([FromQuery] BusinessDetailQuery.Request query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateOrUpdateBusinessData(CreateOrUpdateBusinessData.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }
}

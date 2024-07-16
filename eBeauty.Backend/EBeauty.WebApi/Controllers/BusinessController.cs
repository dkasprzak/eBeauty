using EBeauty.Application.Logic.BusinessFunctions.Commands;
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
    
    [HttpPost]
    public async Task<ActionResult> CreateOrUpdateBusinessData(CreateOrUpdateBusinessData.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }
}

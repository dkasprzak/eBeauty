using EBeauty.Application.Logic.UserFunctions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBeauty.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : BaseController
{
    public UserController(ILogger<UserController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomerUserWithAccount([FromBody] CreateCustomerUserWithAccountCommand.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateBusinessOwnerWithAccount([FromBody] CreateBusinessOwnerWithAccountCommand.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }
}

using EBeauty.Application.Logic.AccountFunctions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBeauty.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountController : BaseController
{
    public AccountController(ILogger<AccountController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetCurrentAccount()
    {
        var result = await _mediator.Send(new CurrentAccountQuery.Request());
        return Ok(result);
    }
}

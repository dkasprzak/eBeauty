using EBeauty.Application.Logic.UserFunctions.Commands;
using EBeauty.Application.Logic.UserFunctions.Queries;
using EBeauty.Infrastructure.Auth;
using EBeauty.WebApi.Application.Responses;
using EBeauty.WebApi.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EBeauty.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : BaseController
{
    private readonly CookieSettings _cookieSettings;
    private readonly JwtManager _jwtManager;

    public UserController(ILogger<UserController> logger, 
        IOptions<CookieSettings> cookieSettings,
        JwtManager jwtManager,
        IMediator mediator) : base(logger, mediator)
    {
        _cookieSettings = cookieSettings != null ? cookieSettings.Value : null;
        _jwtManager = jwtManager;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomerUserWithAccount([FromBody] CreateCustomerUserWithAccountCommand.Request model)
    {
        var result = await _mediator.Send(model);
        var token = _jwtManager.GenerateUserToken(result.UserId);
        SetTokenCookie(token);
        return Ok(new JwtToken { AccessToken = token });
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateBusinessOwnerWithAccount([FromBody] CreateBusinessOwnerWithAccountCommand.Request model)
    {
        var result = await _mediator.Send(model);
        var token = _jwtManager.GenerateUserToken(result.UserId);
        SetTokenCookie(token);
        return Ok(new JwtToken { AccessToken = token });
    }
    
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginCommand.Request model)
    {
        var result = await _mediator.Send(model);
        var token = _jwtManager.GenerateUserToken(result.UserId);
        SetTokenCookie(token);
        return Ok(new JwtToken { AccessToken = token });
    }
    
    [HttpPost]
    public async Task<ActionResult> Logout([FromBody] LogoutCommand.Request model)
    {
        var result = await _mediator.Send(model);
        DeleteTokenCookie();
        return Ok(result);
    }

    private void SetTokenCookie(string token)
    {
        var cookieOption = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(30),
            Secure = true,
            SameSite = SameSiteMode.Lax
        };

        if (_cookieSettings != null)
        {
            cookieOption = new CookieOptions
            {
                HttpOnly = cookieOption.HttpOnly,
                Expires = cookieOption.Expires,
                Secure = _cookieSettings.Secure,
                SameSite = _cookieSettings.SameSite
            };
        }
        Response.Cookies.Append(CookieSettings.CookieName, token, cookieOption);
    }

    [HttpGet]
    public async Task<ActionResult> GetLoggedInUser()
    {
        var result = await _mediator.Send(new LoggedInUserQuery.Request());
        return Ok(result);
    }

    private void DeleteTokenCookie()
    {
        Response.Cookies.Delete(CookieSettings.CookieName, new CookieOptions()
        {
            HttpOnly = true
        });
    }
}

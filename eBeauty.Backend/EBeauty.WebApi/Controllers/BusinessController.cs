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

    [HttpGet]
    public async Task<ActionResult> GetBusinessOpeningHours([FromQuery] OpeningHoursQuery.Request query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetBusinessEmployeeUsersList()
    {
        var result = await _mediator.Send(new BusinessEmployeeUsersListQuery.Request());
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateOrUpdateBusinessData([FromBody] CreateOrUpdateBusinessData.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateBusinessOpeningHours([FromBody] UpdateOpeningHoursCommand.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrUpdateBusinessServices([FromBody] CreateOrUpdateServicesCommand.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteBusinessServices([FromQuery] DeleteBusinessServicesCommand.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetBusinessServices([FromQuery] BusinessServicesQuery.Request query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddOpeningHours([FromBody] AddOpeningHoursCommand.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }
}

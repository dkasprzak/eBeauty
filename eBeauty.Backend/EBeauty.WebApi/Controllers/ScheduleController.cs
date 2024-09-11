﻿using EBeauty.Application.Logic.ScheduleFunctions.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBeauty.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ScheduleController : BaseController
{
    public ScheduleController(ILogger<ScheduleController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    [HttpPost]
    public async Task<ActionResult> AddDayToSchedule([FromBody] AddScheduleCommand.Request model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }
}

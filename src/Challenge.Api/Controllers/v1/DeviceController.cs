using Asp.Versioning;
using Challenge.Application.UseCase.Devices.Base;
using Challenge.Application.UseCase.Devices.Handlers;
using Challenge.Domain.Entity;
using Challenge.Domain.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers.v1;

/// <summary>
/// Controller for device management
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class DeviceController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Create a new device
    /// </summary>
    /// <param name="request">Fields required to create a new device</param>
    /// <returns>Returns a new device with fields</returns>
    [HttpPost("Create")]
    [ProducesResponseType(typeof(Device), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(CreateDeviceUseCase request)
    {
        var response = await mediator.Send(request);

        return Ok(response);
    }

    /// <summary>
    /// Update an existing device
    /// </summary>
    /// <param name="id">The ID of the device to update</param>
    /// <param name="request">Fields available for updating an existing device</param>
    /// <returns>Returns the success of the request</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Device), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] BaseDeviceUseCase request)
    {
        var useCase = request.Adapt<UpdateDeviceUseCase>();
        useCase.Id = id;

        var response = await mediator.Send(useCase);
        return Ok(response);
    }

    /// <summary>
    /// Remove a device by ID
    /// </summary>
    /// <param name="id">The ID of the device to remove</param>
    /// <returns>Returns the success of the request</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Device), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteDeviceUseCase() { Id = id });

        return Ok();
    }

    /// <summary>
    /// Get a single device by ID
    /// </summary>
    /// <param name="id">The ID of the device to retrieve</param>
    /// <returns>Returns a device filtered by ID</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Device), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await mediator.Send(new GetDeviceUseCase() { Id = id });

        return Ok(response);
    }

    /// <summary>
    /// Get a paginated list of all devices
    /// </summary>
    /// <param name="request">Fields available for pagination</param>
    /// <returns>Returns a list of all devices with pagination</returns>
    [HttpGet("All")]
    [ProducesResponseType(typeof(Pagination<Device>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetDeviceAllUseCase request)
    {
        var response = await mediator.Send(request);

        return Ok(response);
    }

    /// <summary>
    /// Get a paginated list of filtered devices
    /// </summary>
    /// <param name="request">Fields available for filtering</param>
    /// <returns>Returns a list of filtered devices with pagination</returns>
    [HttpGet("List")]
    [ProducesResponseType(typeof(List<Device>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] GetDeviceListUseCase request)
    {
        var response = await mediator.Send(request);

        return Ok(response);
    }
}

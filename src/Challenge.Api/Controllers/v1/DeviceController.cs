using Asp.Versioning;
using Challenge.Application.UseCase.Handlers;
using Challenge.Domain.Entity;
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
    ///// <summary>
    ///// Obtém uma lista paginada de todos os contatos
    ///// </summary>
    ///// <param name="useCase">Campos disponíveis para paginação</param>
    ///// <returns>Retorna uma lista de todos os contatos com paginação</returns>
    //[HttpGet("All")]
    //[ProducesResponseType(typeof(Pagination<Contact>), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetAll([FromQuery] GetContactAllUseCase useCase)
    //{
    //    var response = await mediator.Send(useCase);

    //    return Ok(response);
    //}

    ///// <summary>
    ///// Obtém uma lista paginada filtrada de contatos
    ///// </summary>
    ///// <param name="useCase">Campos disponíveis para filtro</param>
    ///// <returns>Retorna uma lista de contatos filtrados com paginação</returns>
    //[HttpGet("List")]
    //[ProducesResponseType(typeof(List<Contact>), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetList([FromQuery] GetContactListUseCase useCase)
    //{
    //    var response = await mediator.Send(useCase);

    //    return Ok(response);
    //}

    /// <summary>
    /// Get a device by ID
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
}

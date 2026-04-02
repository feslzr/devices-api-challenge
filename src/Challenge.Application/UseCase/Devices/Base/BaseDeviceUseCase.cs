using Challenge.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Challenge.Application.UseCase.Devices.Base;

[ExcludeFromCodeCoverage]
public class BaseDeviceUseCase
{
    /// <summary>
    /// Provides the device name.
    /// </summary>
    [JsonPropertyName("name")]
    [BindProperty(Name = "Name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Provides the brand of the device.
    /// </summary>
    [JsonPropertyName("brand")]
    [BindProperty(Name = "Brand")]
    public virtual string? Brand { get; set; }

    /// <summary>
    /// Provides the state of the device. Default value = null.
    /// </summary>
    [JsonPropertyName("state")]
    [BindProperty(Name = "State")]
    [EnumDataType(typeof(StateEnum), ErrorMessage = "The provided state value is not valid.")]
    public virtual StateEnum? State { get; set; } = null;
}
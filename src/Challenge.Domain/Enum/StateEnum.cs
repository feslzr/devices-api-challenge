using System.ComponentModel;

namespace Challenge.Domain.Enum;

#pragma warning disable S2344

public enum StateEnum
{
    [Description("Available")]
    Available = 1,

    [Description("In-use")]
    Inuse = 2,

    [Description("Inactive")]
    Inactive = 3
}
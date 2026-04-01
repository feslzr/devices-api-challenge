using AutoFixture;
using Challenge.Domain.Entity;
using Challenge.Domain.Enum;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Domain.Common;

[ExcludeFromCodeCoverage]
public abstract class BaseDeviceTest
{
    protected readonly IFixture _fixture;

    protected BaseDeviceTest()
    {
        _fixture = new Fixture();

        _fixture.Register(() => Device.Create(
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            (int)StateEnum.Available));
    }
}
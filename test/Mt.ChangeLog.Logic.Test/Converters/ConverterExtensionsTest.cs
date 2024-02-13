using Microsoft.Extensions.DependencyInjection;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Converters;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.Logic.Test.Converters;

/// <summary>
/// Набор тестов для <see cref="ConverterExtensions"/>.
/// </summary>
[TestFixture]
public class ConverterExtensionsTest
{
    private ServiceProvider _provider;

    /// <summary>
    /// Настройка.
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddConverters<LogicLayer>();
        _provider = services.BuildServiceProvider();
    }

    /// <summary>
    /// Заключение.
    /// </summary>
    [OneTimeTearDown]
    public void Down()
    {
        _provider.Dispose();
    }

    /// <summary>
    /// Тест на создание конверторов данных.
    /// </summary>
    /// <param name="converterType">Тип конвертора.</param>
    [TestCase(typeof(IConverter<AnalogModuleEntity, AnalogModuleShortModel>))]
    [TestCase(typeof(IConverter<AnalogModuleEntity, AnalogModuleTableModel>))]
    [TestCase(typeof(IConverter<AnalogModuleEntity, AnalogModuleModel>))]
    [TestCase(typeof(IConverter<PlatformEntity, PlatformShortModel>))]
    [TestCase(typeof(IConverter<PlatformEntity, PlatformTableModel>))]
    [TestCase(typeof(IConverter<PlatformEntity, PlatformModel>))]
    public void BuildConverterTest(Type converterType)
    {
        // act
        var converter = _provider.GetRequiredService(converterType);

        // assert
        converter.Should().NotBeNull();
    }
}
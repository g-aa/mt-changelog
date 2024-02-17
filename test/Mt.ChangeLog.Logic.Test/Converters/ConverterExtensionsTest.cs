using Microsoft.Extensions.DependencyInjection;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.Logic.Converters;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

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
    [TestCase(typeof(IConverter<ArmEditEntity, ArmEditShortModel>))]
    [TestCase(typeof(IConverter<ArmEditEntity, ArmEditModel>))]
    [TestCase(typeof(IConverter<AuthorContributionView, AuthorContributionModel>))]
    [TestCase(typeof(IConverter<AuthorProjectContributionView, AuthorProjectContributionModel>))]
    [TestCase(typeof(IConverter<AuthorEntity, AuthorShortModel>))]
    [TestCase(typeof(IConverter<AuthorEntity, AuthorModel>))]
    [TestCase(typeof(IConverter<CommunicationEntity, CommunicationShortModel>))]
    [TestCase(typeof(IConverter<CommunicationEntity, CommunicationTableModel>))]
    [TestCase(typeof(IConverter<CommunicationEntity, CommunicationModel>))]
    [TestCase(typeof(IConverter<PlatformEntity, PlatformShortModel>))]
    [TestCase(typeof(IConverter<PlatformEntity, PlatformTableModel>))]
    [TestCase(typeof(IConverter<PlatformEntity, PlatformModel>))]
    [TestCase(typeof(IConverter<ProjectStatusEntity, ProjectStatusShortModel>))]
    [TestCase(typeof(IConverter<ProjectStatusEntity, ProjectStatusModel>))]
    [TestCase(typeof(IConverter<ProjectVersionEntity, ProjectVersionShortModel>))]
    [TestCase(typeof(IConverter<ProjectVersionEntity, ProjectVersionTableModel>))]
    [TestCase(typeof(IConverter<ProjectVersionEntity, ProjectVersionModel>))]
    [TestCase(typeof(IConverter<ProjectRevisionEntity, ProjectRevisionShortModel>))]
    [TestCase(typeof(IConverter<ProjectRevisionEntity, ProjectRevisionTableModel>))]
    [TestCase(typeof(IConverter<ProjectRevisionEntity, ProjectRevisionModel>))]
    [TestCase(typeof(IConverter<ProjectRevisionEntity, ProjectRevisionTreeModel>))]
    [TestCase(typeof(IConverter<ProjectRevisionEntity, ProjectRevisionHistoryShortModel>))]
    [TestCase(typeof(IConverter<ProjectRevisionEntity, ProjectRevisionHistoryModel>))]
    [TestCase(typeof(IConverter<LastProjectRevisionView, LastProjectRevisionModel>))]
    [TestCase(typeof(IConverter<LastProjectRevisionView, ProjectRevisionHistoryShortModel>))]
    [TestCase(typeof(IConverter<LastProjectRevisionView, ProjectVersionShortModel>))]
    [TestCase(typeof(IConverter<ProjectHistoryRecordView, ProjectHistoryRecordShortModel>))]
    [TestCase(typeof(IConverter<ProjectHistoryRecordView, ProjectHistoryRecordModel>))]
    [TestCase(typeof(IConverter<ProtocolEntity, ProtocolShortModel>))]
    [TestCase(typeof(IConverter<ProtocolEntity, ProtocolTableModel>))]
    [TestCase(typeof(IConverter<ProtocolEntity, ProtocolModel>))]
    [TestCase(typeof(IConverter<RelayAlgorithmEntity, RelayAlgorithmShortModel>))]
    [TestCase(typeof(IConverter<RelayAlgorithmEntity, RelayAlgorithmModel>))]
    public void BuildConverterTest(Type converterType)
    {
        // act
        var converter = _provider.GetRequiredService(converterType);

        // assert
        converter.Should().NotBeNull();
    }
}
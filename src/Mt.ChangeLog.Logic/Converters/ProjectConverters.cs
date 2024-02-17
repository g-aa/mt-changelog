using System.Globalization;

using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

namespace Mt.ChangeLog.Logic.Converters;

/// <summary>
/// Преобразователь объектов для projects.
/// </summary>
public static class ProjectConverters
{
    /// <summary>
    /// Преобразовать сущность <see cref="ProjectStatusEntity"/> в модель <see cref="ProjectStatusShortModel"/>.
    /// </summary>
    public sealed class StatusEntityToShortModelConverter : IConverter<ProjectStatusEntity, ProjectStatusShortModel>
    {
        /// <inheritdoc />
        public ProjectStatusShortModel Convert(ProjectStatusEntity source)
        {
            return new ProjectStatusShortModel
            {
                Id = source.Id,
                Title = source.Title,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectStatusEntity"/> в модель <see cref="ProjectStatusModel"/>.
    /// </summary>
    public sealed class StatusEntityToModelConverter : IConverter<ProjectStatusEntity, ProjectStatusModel>
    {
        /// <inheritdoc />
        public ProjectStatusModel Convert(ProjectStatusEntity source)
        {
            return new ProjectStatusModel
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectVersionEntity"/> в модель <see cref="ProjectVersionShortModel"/>.
    /// </summary>
    public sealed class VersionEntityToShortModelConverter : IConverter<ProjectVersionEntity, ProjectVersionShortModel>
    {
        /// <inheritdoc />
        public ProjectVersionShortModel Convert(ProjectVersionEntity source)
        {
            return new ProjectVersionShortModel
            {
                Id = source.Id,
                Prefix = source.Prefix,
                Title = source.Title,
                Version = source.Version,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectVersionEntity"/> в модель <see cref="ProjectVersionTableModel"/>.
    /// </summary>
    public sealed class VersionEntityToTableModelConverter : IConverter<ProjectVersionEntity, ProjectVersionTableModel>
    {
        /// <inheritdoc />
        public ProjectVersionTableModel Convert(ProjectVersionEntity source)
        {
            return new ProjectVersionTableModel
            {
                Id = source.Id,
                DIVG = source.DIVG,
                Prefix = source.Prefix,
                Title = source.Title,
                Status = source.ProjectStatus!.Title,
                Version = source.Version,
                Description = source.Description,
                Module = source.AnalogModule!.Title,
                Platform = source.Platform!.Title,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectVersionEntity"/> в модель <see cref="ProjectVersionModel"/>.
    /// </summary>
    public sealed class VersionEntityToModelConverter : IConverter<ProjectVersionEntity, ProjectVersionModel>
    {
        private readonly IConverter<AnalogModuleEntity, AnalogModuleShortModel> _moduleConverter;

        private readonly IConverter<PlatformEntity, PlatformShortModel> _platformConverter;

        private readonly IConverter<ProjectStatusEntity, ProjectStatusShortModel> _statusConverter;

        /// <summary>
        /// Инициализатор нового экземпляра класса <see cref="VersionEntityToModelConverter"/>.
        /// </summary>
        /// <param name="moduleConverter">Конвертер аналоговых модулей.</param>
        /// <param name="platformConverter">Конвертер платформ.</param>
        /// <param name="statusConverter">Конвертер статусов проекта.</param>
        public VersionEntityToModelConverter(
            IConverter<AnalogModuleEntity, AnalogModuleShortModel> moduleConverter,
            IConverter<PlatformEntity, PlatformShortModel> platformConverter,
            IConverter<ProjectStatusEntity, ProjectStatusShortModel> statusConverter)
        {
            _moduleConverter = moduleConverter;
            _platformConverter = platformConverter;
            _statusConverter = statusConverter;
        }

        /// <inheritdoc />
        public ProjectVersionModel Convert(ProjectVersionEntity source)
        {
            return new ProjectVersionModel
            {
                Id = source.Id,
                DIVG = source.DIVG,
                Prefix = source.Prefix,
                Title = source.Title,
                ProjectStatus = _statusConverter.Convert(source.ProjectStatus!),
                Version = source.Version,
                Description = source.Description,
                AnalogModule = _moduleConverter.Convert(source.AnalogModule!),
                Platform = _platformConverter.Convert(source.Platform!),
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionShortModel"/>.
    /// </summary>
    public sealed class RevisionEntityToShortModelConverter : IConverter<ProjectRevisionEntity, ProjectRevisionShortModel>
    {
        /// <inheritdoc />
        public ProjectRevisionShortModel Convert(ProjectRevisionEntity source)
        {
            return new ProjectRevisionShortModel
            {
                Id = source.Id,
                Prefix = source.ProjectVersion!.Prefix,
                Title = source.ProjectVersion!.Title,
                Version = source.ProjectVersion!.Version,
                Revision = source.Revision,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionTableModel"/>.
    /// </summary>
    public sealed class RevisionEntityToTableModelConverter : IConverter<ProjectRevisionEntity, ProjectRevisionTableModel>
    {
        /// <inheritdoc />
        public ProjectRevisionTableModel Convert(ProjectRevisionEntity source)
        {
            return new ProjectRevisionTableModel
            {
                Id = source.Id,
                Prefix = source.ProjectVersion!.Prefix,
                Title = source.ProjectVersion!.Title,
                Version = source.ProjectVersion!.Version,
                Revision = source.Revision,
                Date = source.Date,
                ArmEdit = source.ArmEdit!.Version,
                Reason = source.Reason,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionModel"/>.
    /// </summary>
    public sealed class RevisionEntityToModelConverter : IConverter<ProjectRevisionEntity, ProjectRevisionModel>
    {
        private readonly IConverter<ProjectRevisionEntity, ProjectRevisionShortModel> _revisionConverter;

        private readonly IConverter<ProjectVersionEntity, ProjectVersionShortModel> _versionConverter;

        private readonly IConverter<ArmEditEntity, ArmEditShortModel> _armEditConverter;

        private readonly IConverter<CommunicationEntity, CommunicationShortModel> _communicationConverter;

        private readonly IConverter<AuthorEntity, AuthorShortModel> _authorConverter;

        private readonly IConverter<RelayAlgorithmEntity, RelayAlgorithmShortModel> _algorithmConverter;

        /// <summary>
        /// Инициализатор нового экземпляра класса <see cref="RevisionEntityToModelConverter"/>.
        /// </summary>
        /// <param name="revisionConverter">Конвертер ревизий проекта.</param>
        /// <param name="versionConverter">Конвертер версий проекта.</param>
        /// <param name="armEditConverter">Конвертер arm edit.</param>
        /// <param name="communicationConverter">Конвертер коммуникационных адаптеров.</param>
        /// <param name="authorConverter">Конвертер автора проекта.</param>
        /// <param name="algorithmConverter">Конвертер алгоритма.</param>
        public RevisionEntityToModelConverter(
            IConverter<ProjectRevisionEntity, ProjectRevisionShortModel> revisionConverter,
            IConverter<ProjectVersionEntity, ProjectVersionShortModel> versionConverter,
            IConverter<ArmEditEntity, ArmEditShortModel> armEditConverter,
            IConverter<CommunicationEntity, CommunicationShortModel> communicationConverter,
            IConverter<AuthorEntity, AuthorShortModel> authorConverter,
            IConverter<RelayAlgorithmEntity, RelayAlgorithmShortModel> algorithmConverter)
        {
            _revisionConverter = revisionConverter;
            _versionConverter = versionConverter;
            _armEditConverter = armEditConverter;
            _communicationConverter = communicationConverter;
            _authorConverter = authorConverter;
            _algorithmConverter = algorithmConverter;
        }

        /// <inheritdoc />
        public ProjectRevisionModel Convert(ProjectRevisionEntity source)
        {
            return new ProjectRevisionModel
            {
                Id = source.Id,
                Date = source.Date,
                Revision = source.Revision,
                Reason = source.Reason,
                Description = source.Description,
                ParentRevision = source.ParentRevision != null ? _revisionConverter.Convert(source.ParentRevision!) : null,
                ProjectVersion = _versionConverter.Convert(source.ProjectVersion!),
                ArmEdit = _armEditConverter.Convert(source.ArmEdit!),
                Communication = _communicationConverter.Convert(source.Communication!),
                Authors = source.Authors.Select(_authorConverter.Convert).ToList(),
                RelayAlgorithms = source.RelayAlgorithms.Select(_algorithmConverter.Convert).ToList(),
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionTreeModel"/>.
    /// </summary>
    public sealed class RevisionEntityToTreeModelConverter : IConverter<ProjectRevisionEntity, ProjectRevisionTreeModel>
    {
        /// <inheritdoc />
        public ProjectRevisionTreeModel Convert(ProjectRevisionEntity source)
        {
            return new ProjectRevisionTreeModel
            {
                Id = source.Id,
                ParentId = source.ParentRevisionId,
                Prefix = source.ProjectVersion!.Prefix,
                Title = source.ProjectVersion!.Title,
                Version = source.ProjectVersion!.Version,
                Revision = source.Revision,
                Date = source.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                ArmEdit = source.ArmEdit!.Version,
                Platform = source.ProjectVersion!.Platform!.Title,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionHistoryShortModel"/>.
    /// </summary>
    public sealed class RevisionEntityToHistoryShortModelConverter : IConverter<ProjectRevisionEntity, ProjectRevisionHistoryShortModel>
    {
        /// <inheritdoc />
        public ProjectRevisionHistoryShortModel Convert(ProjectRevisionEntity source)
        {
            return new ProjectRevisionHistoryShortModel
            {
                Id = source.Id,
                Date = source.Date,
                Title = $"{source.ProjectVersion!.Prefix}-{source.ProjectVersion!.Title}-{source.ProjectVersion!.Version}_{source.Revision}",
                Platform = source.ProjectVersion!.Platform!.Title,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionHistoryModel"/>.
    /// </summary>
    public sealed class RevisionEntityToHistoryModelConverter : IConverter<ProjectRevisionEntity, ProjectRevisionHistoryModel>
    {
        /// <inheritdoc />
        public ProjectRevisionHistoryModel Convert(ProjectRevisionEntity source)
        {
            return new ProjectRevisionHistoryModel
            {
                Id = source.Id,
                ArmEdit = source.ArmEdit!.Version,
                Authors = source.Authors.Select(a => $"{a.FirstName} {a.LastName}").ToList(),
                RelayAlgorithms = source.RelayAlgorithms.Select(ra => ra.Title).ToList(),
                Communication = string.Join(", ", source.Communication!.Protocols.OrderBy(e => e.Title).Select(e => e.Title)),
                Date = source.Date,
                Description = source.Description,
                Platform = source.ProjectVersion!.Platform!.Title,
                Reason = source.Reason,
                Title = $"{source.ProjectVersion!.Prefix}-{source.ProjectVersion!.Title}-{source.ProjectVersion!.Version}_{source.Revision}",
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="LastProjectRevisionView"/> в модель <see cref="LastProjectRevisionModel"/>.
    /// </summary>
    public sealed class LastRevisionEntityToModelConverter : IConverter<LastProjectRevisionView, LastProjectRevisionModel>
    {
        /// <inheritdoc />
        public LastProjectRevisionModel Convert(LastProjectRevisionView source)
        {
            return new LastProjectRevisionModel
            {
                Prefix = source.Prefix,
                Title = source.Title,
                Version = source.Version,
                Revision = source.Revision,
                Platform = source.Platform,
                ArmEdit = source.ArmEdit,
                Date = source.Date,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="LastProjectRevisionView"/> в модель <see cref="ProjectRevisionHistoryShortModel"/>.
    /// </summary>
    public sealed class LastRevisionEntityToHistoryShortModelConverter : IConverter<LastProjectRevisionView, ProjectRevisionHistoryShortModel>
    {
        /// <inheritdoc />
        public ProjectRevisionHistoryShortModel Convert(LastProjectRevisionView source)
        {
            return new ProjectRevisionHistoryShortModel
            {
                Id = source.ProjectRevisionId,
                Date = source.Date,
                Platform = source.Platform,
                Title = source.ToString(),
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="LastProjectRevisionView"/> в модель <see cref="ProjectVersionShortModel"/>.
    /// </summary>
    public sealed class LastRevisionEntityToVersionShortModelConverter : IConverter<LastProjectRevisionView, ProjectVersionShortModel>
    {
        /// <inheritdoc />
        public ProjectVersionShortModel Convert(LastProjectRevisionView source)
        {
            return new ProjectVersionShortModel
            {
                Id = source.ProjectVersionId,
                Prefix = source.Prefix,
                Title = source.Title,
                Version = source.Version,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectHistoryRecordView"/> в модель <see cref="ProjectHistoryRecordShortModel"/>.
    /// </summary>
    public sealed class HistoryRecordEntityToShortModelConverter : IConverter<ProjectHistoryRecordView, ProjectHistoryRecordShortModel>
    {
        /// <inheritdoc />
        public ProjectHistoryRecordShortModel Convert(ProjectHistoryRecordView source)
        {
            return new ProjectHistoryRecordShortModel
            {
                ProjectRevisionId = source.ProjectRevisionId,
                Title = source.Title,
                Date = source.Date,
                Platform = source.Platform,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectHistoryRecordView"/> в модель <see cref="ProjectHistoryRecordModel"/>.
    /// </summary>
    public sealed class HistoryRecordEntityToModelConverter : IConverter<ProjectHistoryRecordView, ProjectHistoryRecordModel>
    {
        /// <inheritdoc />
        public ProjectHistoryRecordModel Convert(ProjectHistoryRecordView source)
        {
            return new ProjectHistoryRecordModel
            {
                ProjectRevisionId = source.ProjectRevisionId,
                ParentRevisionId = source.ParentRevisionId,
                ProjectVersionId = source.ProjectVersionId,
                Title = source.Title,
                Date = source.Date,
                Platform = source.Platform,
                ArmEdit = source.ArmEdit,
                Authors = source.Authors,
                Protocols = source.Protocols,
                Algorithms = source.Algorithms,
                Reason = source.Reason,
                Description = source.Description,
            };
        }
    }
}
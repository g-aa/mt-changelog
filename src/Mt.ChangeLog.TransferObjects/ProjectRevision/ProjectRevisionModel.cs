using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.Utilities;
using System;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    /// <summary>
    /// Полная модель редакции проекта.
    /// </summary>
    public class ProjectRevisionModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Дата компиляции.
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Редакция.
        /// </summary>
        public string Revision { get; set; }
        
        /// <summary>
        /// Причина изменений.
        /// </summary>
        public string Reason { get; set; }
        
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Версия проекта.
        /// </summary>
        public ProjectVersionShortModel ProjectVersion { get; set; }
        
        /// <summary>
        /// Родительская редакция.
        /// </summary>
        public ProjectRevisionShortModel ParentRevision { get; set; }
        
        /// <summary>
        /// Коммуникационный модуль.
        /// </summary>
        public CommunicationShortModel Communication { get; set; }

        /// <summary>
        /// Версия ArmEdit.
        /// </summary>
        public ArmEditShortModel ArmEdit { get; set; }
        
        /// <summary>
        /// Перечень авторов.
        /// </summary>
        public IEnumerable<AuthorShortModel> Authors { get; set; }
        
        /// <summary>
        /// Перечень алгоритмов.
        /// </summary>
        public IEnumerable<RelayAlgorithmShortModel> RelayAlgorithms { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectRevisionModel"/>.
        /// </summary>
        public ProjectRevisionModel()
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
            this.Revision = DefaultString.Revision;
            this.Reason = DefaultString.Reason;
            this.Description = DefaultString.Description;
            this.ProjectVersion = new ProjectVersionShortModel();
            this.ParentRevision = new ProjectRevisionShortModel();
            this.Communication = new CommunicationShortModel();
            this.ArmEdit = new ArmEditShortModel();
            this.Authors = new HashSet<AuthorShortModel>();
            this.RelayAlgorithms = new HashSet<RelayAlgorithmShortModel>();
        }
    }
}
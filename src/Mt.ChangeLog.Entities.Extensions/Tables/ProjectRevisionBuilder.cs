using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.Utilities;
using System;
using System.Linq;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="ProjectRevision"/>.
    /// </summary>
    public class ProjectRevisionBuilder
    {
        private readonly ProjectRevision entity;

        private DateTime? date;
        private string revision;
        private string reason;
        private string description;
        private ProjectVersion project;
        private ProjectRevision parent;
        private ArmEdit armedit;
        private Communication communication;
        private IQueryable<Author> authors;
        private IQueryable<RelayAlgorithm> algorithms;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ProjectRevisionBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если entity равно null.</exception>
        public ProjectRevisionBuilder(ProjectRevision entity) 
        {
            this.entity = Check.NotNull(entity, nameof(entity));
            this.date = entity.Date;
            this.revision = entity.Revision;
            this.reason = entity.Reason;
            this.description = entity.Description;
            this.project = entity.ProjectVersion;
            this.parent = entity.ParentRevision;
            this.armedit = entity.ArmEdit;
            this.communication = entity.Communication;
            this.authors = entity.Authors.AsQueryable();
            this.algorithms = entity.RelayAlgorithms.AsQueryable();
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если model равно null.</exception>
        public ProjectRevisionBuilder SetAttributes(ProjectRevisionModel model)
        {
            Check.NotNull(model, nameof(model));
            this.date = model.Date;
            this.revision = model.Revision;
            this.reason = model.Reason;
            this.description = model.Description;
            return this;
        }

        /// <summary>
        /// Добавить версию проекта.
        /// </summary>
        /// <param name="project">Версия проекта.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если project равно null.</exception>
        public ProjectRevisionBuilder SetProjectVersion(ProjectVersion project) 
        {
            this.project = Check.NotNull(project, nameof(project));
            return this;
        }

        /// <summary>
        /// Добавить родительскую редакцию.
        /// </summary>
        /// <param name="parent">Родительская редакция.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если parent равно null.</exception>
        public ProjectRevisionBuilder SetParentRevision(ProjectRevision parent) 
        {
            this.parent = Check.NotNull(parent, nameof(parent));
            return this;
        }

        /// <summary>
        /// Добавить ArmEdit.
        /// </summary>
        /// <param name="armedit">ArmEdit.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если armedit равно null.</exception>
        public ProjectRevisionBuilder SetArmEdit(ArmEdit armedit)
        {
            this.armedit = Check.NotNull(armedit, nameof(armedit));
            return this;
        }

        /// <summary>
        /// Добавить АК.
        /// </summary>
        /// <param name="communication">АК.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если communication равно null.</exception>
        public ProjectRevisionBuilder SetCommunication(Communication communication)
        {
            this.communication = Check.NotNull(communication, nameof(communication));
            return this;
        }

        /// <summary>
        /// Добавить перечень авторов.
        /// </summary>
        /// <param name="authors">Перечень авторов.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если authors равно null.</exception>
        public ProjectRevisionBuilder SetAuthors(IQueryable<Author> authors)
        {
            this.authors = Check.NotNull(authors, nameof(authors));
            return this;
        }

        /// <summary>
        /// Добавить перечень алгоритмов.
        /// </summary>
        /// <param name="algorithms">Перечень алгоритмов.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если algorithms равно null.</exception>
        public ProjectRevisionBuilder SetAlgorithms(IQueryable<RelayAlgorithm> algorithms)
        {
            this.algorithms = Check.NotNull(algorithms, nameof(algorithms));
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public ProjectRevision Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Date = this.date != null ? this.date.Value : DateTime.Now;
            // - не обновляется!
            if (string.IsNullOrEmpty(this.entity.Revision)) 
            {
                this.entity.Revision = this.revision;     
            }
            this.entity.Reason = this.reason;
            this.entity.Description = description;
            // реляционные связи:
            // - не обновляется!
            if (this.entity.ProjectVersion is null) 
            {
                this.entity.ProjectVersion = this.project;    
            }
            this.entity.ParentRevision = this.parent;
            this.entity.ArmEdit = this.armedit;
            this.entity.Communication = this.communication;
            this.entity.Authors = this.authors.ToHashSet();
            this.entity.RelayAlgorithms = this.algorithms.ToHashSet();
            return this.entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static ProjectRevisionBuilder GetBuilder()
        {
            return new ProjectRevisionBuilder(new ProjectRevision());
        }
    }
}
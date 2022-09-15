using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.Utilities;
using System;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Other
{
    /// <summary>
    /// Статистика ChangeLog.
    /// </summary>
    public struct StatisticsModel
    {
        /// <summary>
        /// Дата сбора статистики.
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Актуальная версия ArmEdit.
        /// </summary>
        public string ArmEdit { get; set; }
        
        /// <summary>
        /// Количество проектов.
        /// </summary>
        public int ProjectCount { get; set; }

        /// <summary>
        /// Распространение проектов.
        /// </summary>
        public Dictionary<string, int> ProjectDistributions { get; set; }

        /// <summary>
        /// Модель автор общий вклад в проекты.
        /// </summary>
        public IEnumerable<AuthorContributionModel> AuthorContributions { get; set; }
        
        /// <summary>
        /// Последние изменения по проектам.
        /// </summary>
        public IEnumerable<ProjectRevisionHistoryShortModel> LastModifiedProjects { get; set; }

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="StatisticsModel"/>
        /// </summary>
        public StatisticsModel()
        {
            this.Date = DateTime.Now;
            this.ArmEdit = DefaultString.Version;
            this.ProjectCount = 0;
            this.ProjectDistributions = new Dictionary<string, int>();
            this.AuthorContributions = Array.Empty<AuthorContributionModel>();
            this.LastModifiedProjects = Array.Empty<ProjectRevisionHistoryShortModel>();
        }
    }
}
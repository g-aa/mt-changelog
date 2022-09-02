using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Historical;
using System;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Other
{
    /// <summary>
    /// Статистика ChangeLog.
    /// </summary>
    public class StatisticsModel
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
    }
}
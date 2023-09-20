using System.Text;

using Mt.ChangeLog.TransferObjects.Other;

namespace Mt.ChangeLog.TransferObjects.Historical;

/// <summary>
/// Модель файла истории проекта.
/// </summary>
public sealed class ProjectHistoryFileModel : FileModel
{
    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProjectHistoryFileModel"/>.
    /// </summary>
    /// <param name="model">Модель истории версии проекта.</param>
    public ProjectHistoryFileModel(ProjectVersionHistoryModel model)
        : base($"ChangeLog-{model?.Title}.txt", Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, model?.History.Select(e => e.ToText()))))
    {
    }
}
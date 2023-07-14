namespace Mt.ChangeLog.TransferObjects.ProjectStatus;

/// <summary>
/// Полная модель статуса проекта.
/// </summary>
public class ProjectStatusModel : ProjectStatusTableModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectStatusModel"/>.
    /// </summary>
    public ProjectStatusModel() : base()
    {
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return base.ToString();
    }
}
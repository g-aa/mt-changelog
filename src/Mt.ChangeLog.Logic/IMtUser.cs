namespace Mt.ChangeLog.Logic;

/// <summary>
/// Пользователь системы МТ.
/// </summary>
public interface IMtUser
{
    /// <summary>
    /// Наименование пользователя системы.
    /// </summary>
    string Name { get; }
}
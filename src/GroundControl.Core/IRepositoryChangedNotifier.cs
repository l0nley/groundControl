namespace GroundControl.Core
{
  /// <summary>
  /// Repository changed notifier interface
  /// </summary>
  public interface IRepositoryChangedNotifier
  {
    /// <summary>
    /// Fires when repository changes
    /// </summary>
    event RepositoryChangedDelegate RepositoryChanged;
  }

  public delegate void RepositoryChangedDelegate(object sender);
}
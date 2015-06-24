namespace GroundControl.Core
{
  /// <summary>
  /// Connection endpoint description
  /// </summary>
  public interface IConnectionEndpoint
  {
    /// <summary>
    /// Uri to connect
    /// </summary>
    string Uri { get; }
  }
}

using System;

namespace GroundControl.Core
{
  public interface IHealthDescription
  {
    /// <summary>
    /// the Timestampt
    /// </summary>
    DateTime Timestamp { get; }

    /// <summary>
    /// Health name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Health information value
    /// </summary>
    object Value { get; }
  }
}
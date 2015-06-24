using System;

namespace GroundControl.Core
{
  public class HealthDescription : IHealthDescription
  {
    public HealthDescription(string name, object value)
    {
      Name = name;
      Value = value;
      Timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// the Timestampt
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// Health name
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Health information value
    /// </summary>
    public object Value { get; protected set; }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>
    /// A string that represents the current object.
    /// </returns>
    public override string ToString()
    {
      return $"{Name}:{Value}";
    }
  }
}
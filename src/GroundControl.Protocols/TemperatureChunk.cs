using System;
using System.Globalization;

namespace GroundControl.Protocols
{
  /// <summary>
  /// Chunk description for temperature
  /// </summary>
  public class TemperatureChunk : FixedSizeChunkBase
  {
    public TemperatureChunk()
      : base((byte)'T', sizeof(float), AggregateHelpers.AverageFloat)
    {
      Description = "Temperature";
    }

    /// <summary>
    /// Converts value of chunk to human readble format
    /// </summary>
    /// <param name="value">The value</param>
    /// <returns>the string</returns>
    public override string ToHuman(byte[] value) => BitConverter.ToSingle(value, 0).ToString("000 °C",CultureInfo.InvariantCulture);
  }
}
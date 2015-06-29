using System;
using System.Globalization;

namespace GroundControl.Protocols
{
  /// <summary>
  /// Distance chunk
  /// </summary>
  public class DistanceChunk : FixedSizeChunkBase
  {
    public DistanceChunk()
      : base((byte)'D',sizeof(float), AggregateHelpers.AverageFloat)
    {
      Description = "Distance";
    }

    /// <summary>
    /// Converts value of chunk to human readble format
    /// </summary>
    /// <param name="value">The value</param>
    /// <returns>the string</returns>
    public override string ToHuman(byte[] value) => BitConverter.ToSingle(value,0).ToString("000 cm", CultureInfo.InvariantCulture);
  }
}
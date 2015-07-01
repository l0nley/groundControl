using System;
using System.Globalization;

namespace GroundControl.Protocols
{
  public class SecondMotorChunk : FixedSizeChunkBase
  {
    public SecondMotorChunk() 
      : base((byte)'B', sizeof(short), AggregateHelpers.AverageShort)
    {
      Description = "Motor B";
    }

    public override string ToHuman(byte[] value) =>
       BitConverter.ToInt16(value, 0).ToString("0000 ", CultureInfo.InvariantCulture) + "mA";
  }
}

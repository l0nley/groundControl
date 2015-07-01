using System;
using System.Globalization;

namespace GroundControl.Protocols
{
  public class FirstMotorChunk : FixedSizeChunkBase
  {
    public FirstMotorChunk() 
      : base((byte)'A', sizeof(short), AggregateHelpers.AverageShort)
    {
      Description = "Motor A";
    }

    public override string ToHuman(byte[] value) =>
       BitConverter.ToInt16(value, 0).ToString("0000 ", CultureInfo.InvariantCulture) + "mA";
  }
}

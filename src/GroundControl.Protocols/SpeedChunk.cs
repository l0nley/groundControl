using System.Globalization;

namespace GroundControl.Protocols
{
  public class SpeedChunk : FixedSizeChunkBase
  {
    public SpeedChunk() 
      : base((byte)'S', sizeof(byte), AggregateHelpers.AverageByte)
    {
      Description = "Speed";
    }

    public override string ToHuman(byte[] value) =>
       value[0].ToString("000", CultureInfo.InvariantCulture) + " dtc";
  }
}

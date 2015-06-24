namespace GroundControl.Core
{
  /// <summary>
  /// Simple chunk 
  /// </summary>
  public class Chunk : IChunk
  {
    public Chunk(IChunkDescription description, byte[] value)
    {
      Description = description;
      Value = value;
    }
    /// <summary>
    /// Description of the chunk
    /// </summary>
    public IChunkDescription Description { get; }

    /// <summary>
    /// Raw chunk value
    /// </summary>
    public byte[] Value { get; }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>
    /// A string that represents the current object.
    /// </returns>
    public override string ToString() => $"{Description.ShortName}:{Description.ToHuman(Value)}";
  }
}
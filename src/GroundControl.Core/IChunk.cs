namespace GroundControl.Core
{
  /// <summary>
  /// Data chunk
  /// </summary>
  public interface IChunk
  {
    /// <summary>
    /// Description of the chunk
    /// </summary>
    IChunkDescription Description { get; }

    /// <summary>
    /// Raw chunk value
    /// </summary>
    byte[] Value { get; }
  }
}
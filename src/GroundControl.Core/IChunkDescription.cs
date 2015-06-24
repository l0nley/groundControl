using System.Collections.Generic;

namespace GroundControl.Core
{
  /// <summary>
  /// Chunk description
  /// </summary>
  public interface IChunkDescription
  {
    /// <summary>
    /// Chunk description
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Short name of chunk
    /// </summary>
    string ShortName { get; }

    /// <summary>
    /// Chunk prefix
    /// </summary>
    byte Prefix { get; }

    /// <summary>
    /// Is fixed size
    /// </summary>
    bool FixedSize { get; }

    /// <summary>
    /// If chunk uses fixed size, this will contain bytes to read the chunk
    /// </summary>
    int ByteLength { get; }

    /// <summary>
    /// If chunk is not fixed size, this byte will flag for reading termination
    /// </summary>
    byte Terminator { get; }

    /// <summary>
    /// Aggregate operation
    /// </summary>
    /// <param name="chunksData">Chunks to aggregate</param>
    /// <returns>Aggregated value</returns>
    byte[] Aggregate(IEnumerable<byte[]> chunksData);

    /// <summary>
    /// Converts value of chunk to human readble format
    /// </summary>
    /// <param name="value">The value</param>
    /// <returns>the string</returns>
    string ToHuman(byte[] value);
  }
}
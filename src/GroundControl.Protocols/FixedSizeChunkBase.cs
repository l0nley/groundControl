using System.Collections.Generic;
using GroundControl.Core;

namespace GroundControl.Protocols
{
  public abstract class FixedSizeChunkBase : IChunkDescription
  {
    private readonly AggregateOperation _aggregate;

    protected FixedSizeChunkBase(byte prefix, int size, AggregateOperation aggregate)
    {
      _aggregate = aggregate;
      Prefix = prefix;
      FixedSize = true;
      ByteLength = size;
      Terminator = 0;
    }

    /// <summary>
    /// Chunk description
    /// </summary>
    public string Description { get; protected set; }

    /// <summary>
    /// Short name of chunk
    /// </summary>
    public virtual string ShortName => $"{(char)Prefix}";

    /// <summary>
    /// Chunk prefix
    /// </summary>
    public byte Prefix { get; }

    /// <summary>
    /// Is fixed size
    /// </summary>
    public bool FixedSize { get; }

    /// <summary>
    /// If chunk uses fixed size, this will contain bytes to read the chunk
    /// </summary>
    public int ByteLength { get; }

    /// <summary>
    /// If chunk is not fixed size, this byte will flag for reading termination
    /// </summary>
    public byte Terminator { get; }

    /// <summary>
    /// Aggregate operation
    /// </summary>
    /// <param name="chunksData">Chunks to aggregate</param>
    /// <returns>Aggregated value</returns>
    public byte[] Aggregate(IEnumerable<byte[]> chunksData)
    {
      return _aggregate(chunksData);
    }

    /// <summary>
    /// Converts value of chunk to human readble format
    /// </summary>
    /// <param name="value">The value</param>
    /// <returns>the string</returns>
    public abstract string ToHuman(byte[] value);
  }

  public delegate byte[] AggregateOperation(IEnumerable<byte[]> chunks);
}
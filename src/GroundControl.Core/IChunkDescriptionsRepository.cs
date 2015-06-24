using System;
using System.Collections.Generic;

namespace GroundControl.Core
{
  /// <summary>
  /// Chunk description operator interface
  /// </summary>
  public interface IChunkDescriptionsRepository : IDictionary<byte, IChunkDescription>, ICloneable, IRepositoryChangedNotifier
  {
  }
}
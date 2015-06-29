using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GroundControl.Core;

namespace GroundControl.Connections
{
  public class BuzzerConnection : Connection
  {
    private readonly Random _random;
    private readonly Queue<byte> _queue;
    internal BuzzerConnection(IConnectionEndpoint endpoint)
      : base(endpoint)
    {
      _random = new Random();
      _queue = new Queue<byte>();
    }

    /// <summary>
    /// Opens connection
    /// </summary>
    /// <returns>The Task</returns>
    public override Task Open()
    {
      if (IsOpened)
      {
        throw new InvalidOperationException();
      }
      IsOpened = true;
      return Task.FromResult(0);
    }

    /// <summary>
    /// Indicates is stream opened;
    /// </summary>
    public override bool IsOpened { get; protected set; }

    /// <summary>
    /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
    public override void Flush()
    {
    }

    /// <summary>
    /// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. Instead of calling this method, ensure that the stream is properly disposed.
    /// </summary>
    public override void Close()
    {
      IsOpened = false;
      base.Close();
    }

    /// <summary>
    /// When overridden in a derived class, sets the position within the current stream.
    /// </summary>
    /// <returns>
    /// The new position within the current stream.
    /// </returns>
    /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter. </param><param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position. </param><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// When overridden in a derived class, sets the length of the current stream.
    /// </summary>
    /// <param name="value">The desired length of the current stream in bytes. </param><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override void SetLength(long value)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <returns>
    /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
    /// </returns>
    /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source. </param><param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream. </param><param name="count">The maximum number of bytes to be read from the current stream. </param><exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length. </exception><exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.NotSupportedException">The stream does not support reading. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override int Read(byte[] buffer, int offset, int count)
    {
      if (IsOpened == false)
      {
        throw new InvalidOperationException();
      }
      if (_queue.Count == 0 || count > _queue.Count)
      {
        var lst = new List<byte> { (byte)'H' };
        lst.AddRange(BitConverter.GetBytes(_random.Next(100, 999) / 10.0f));
        lst.Add((byte)'T');
        lst.AddRange(BitConverter.GetBytes(_random.Next(10, 300) / 10.0f));
        lst.Add((byte)'D');
        lst.AddRange(BitConverter.GetBytes((float)_random.Next(0, 400)));
        lst.InsertRange(0, BitConverter.GetBytes(lst.Count));
        foreach (var @byte in lst)
        {
          _queue.Enqueue(@byte);
        }
      }

      var result = new byte[count];
      for (var i = 0; i < count; i++)
      {
        result[i] = _queue.Dequeue();
      }
      Array.Copy(result, 0, buffer, offset, count);
      return result.Length;
    }

    /// <summary>
    /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
    /// </summary>
    /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream. </param><param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream. </param><param name="count">The number of bytes to be written to the current stream. </param>
    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
    /// </summary>
    /// <returns>
    /// true if the stream supports reading; otherwise, false.
    /// </returns>
    public override bool CanRead => true;

    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
    /// </summary>
    /// <returns>
    /// true if the stream supports seeking; otherwise, false.
    /// </returns>
    public override bool CanSeek => false;

    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
    /// </summary>
    /// <returns>
    /// true if the stream supports writing; otherwise, false.
    /// </returns>
    public override bool CanWrite => false;

    /// <summary>
    /// When overridden in a derived class, gets the length in bytes of the stream.
    /// </summary>
    /// <returns>
    /// A long value representing the length of the stream in bytes.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override long Length
    {
      get { throw new NotSupportedException(); }
    }



    /// <summary>
    /// When overridden in a derived class, gets or sets the position within the current stream.
    /// </summary>
    /// <returns>
    /// The current position within the stream.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override long Position
    {
      get { throw new NotSupportedException(); }
      set { throw new NotSupportedException(); }
    }
  }
}

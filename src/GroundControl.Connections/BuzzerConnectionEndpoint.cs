using System;

namespace GroundControl.Connections
{
  public class BuzzerConnectionEndpoint : ConnectionEndpoint
  {
    public BuzzerConnectionEndpoint() : base($"device://buzzer{new Random().Next(0,100)}")
    {
    }
  }
}
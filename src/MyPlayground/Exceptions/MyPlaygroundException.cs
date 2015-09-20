using System;

namespace MyPlayground.Exceptions
{
  public class MyPlaygroundException : Exception
  {
    public MyPlaygroundException(string message)
      : base(message)
    { }
  }
}

using System;

namespace CM.DL.FilesExceptions
{
  public class DuplicateUserCredentialsException : Exception
  {
    public DuplicateUserCredentialsException(string details) : base(details)
    {
    }
  }
}
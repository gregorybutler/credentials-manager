using System;

namespace CM.DL.FilesExceptions
{
  public class UsersNotFoundException : Exception
  {
    public UsersNotFoundException(string file, Exception inner)
      : base($"{file} file not found or not able to open!", inner)
    {
    }
  }
}
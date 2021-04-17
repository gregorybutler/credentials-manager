using System;
using System.IO;
using System.Security;
using CM.DL.FilesExceptions;
using CM.Common;

namespace CM.DL
{
  public static class Files
  {
    private static readonly string[] Delimiters = { "\r\n", "\n" };

    public static string ReadAllText(string file)
    {
      try
      {
        using (var reader = new StreamReader(file))
        {
          return reader.ReadToEnd();
        }
      }
      catch (Exception ex) when (ex is ArgumentException
                              or ArgumentNullException
                              or FileNotFoundException
                              or DirectoryNotFoundException
                              or IOException)
      {
        throw new UsersNotFoundException(file, ex);
      }
    }

    public static string[] ReadAllLines(string file)
    {
      var content = ReadAllText(file);
      return content.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
    }

    public static void WriteAllText(string file, string data)
    {
      try
      {
        using (var writer = new StreamWriter(file))
        {
          writer.Write(data);
        }
      }
      catch (Exception ex) when (ex is UnauthorizedAccessException
                              or ArgumentException
                              or ArgumentNullException
                              or DirectoryNotFoundException
                              or PathTooLongException
                              or IOException
                              or SecurityException)
      {
        throw new UsersNotFoundException(file, ex);
      }
    }

    public static void WriteAllLines(string file, string[] data, bool append = false)
    {
      if (data.Length == 0)
      {
        WriteAllText(file, string.Empty);
        return;
      }

      WriteLine(file, data[0]);

      for (var i = 1; i < data.Length; i++)
      {
        WriteLine(file, data[i], append);
      }
    }

    public static void DeleteLine(string file, string data)
    {
      var contents = ReadAllLines(file);
      var index = ArrayHelper.GetIndex(contents, data);

      var output = ArrayHelper.RemoveAt(contents, index);
      WriteAllLines(file, output, true);
    }

    public static void WriteLine(string file, string data, bool append = false)
    {
      try
      {
        using (var writer = new StreamWriter(file, append))
        {
          writer.WriteLine(data);
        }
      }
      catch (Exception ex) when (ex is UnauthorizedAccessException
                              or ArgumentException
                              or ArgumentNullException
                              or DirectoryNotFoundException
                              or PathTooLongException
                              or IOException
                              or SecurityException)
      {
        throw new UsersNotFoundException(file, ex);
      }
    }
  }
}
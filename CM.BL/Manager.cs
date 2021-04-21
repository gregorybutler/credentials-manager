using System.Collections.Generic;
using System.IO;
using System.Text;
using CM.DL;
using CM.DL.FilesExceptions;

namespace CM.BL
{
  public static class Manager
  {
    private const string FieldDelimiter = ",";
     
    private static string CredentialsFile;

    private static List<Credentials> _credentials = new();

    public static void Initialize()
    {
      CredentialsFile = "users.txt";
      _credentials = GetCredentials();

      CheckForDuplicates();
      System.Console.WriteLine("test");
    }

    private static List<Credentials> GetCredentials()
    {
      var content = Files.ReadAllLines(CredentialsFile);
      var credentials = new List<Credentials>();

      foreach (var row in content)
      {
        var isValidCredential = Credentials.TryParse(row, FieldDelimiter, out var credential);
        if (isValidCredential)
        {
          credentials.Add(credential);
        }
      }

      return credentials;
    }

    private static void CheckForDuplicates()
    {
      _credentials.Sort();

      for (var index = 0; index + 1 < _credentials.Count; index++)
      {
        var currentCredential = _credentials[index];
        var nextCredential = _credentials[index + 1];

        if (currentCredential.CompareTo(nextCredential).Equals(0))
        {
          throw new DuplicateUserCredentialsException("Duplicate user names found!");
        }
      }
    }

    public static bool Login(Credentials credentials)
    {
      return _credentials.Contains(credentials);
    }

    public static bool Register(Credentials credentials)
    {
      if (!Find(credentials))
      {
        Files.WriteLine(CredentialsFile, ToString(credentials), true);
        return true;
      }

      return false;
    }

    public static bool Delete(string userName)
    {
      var credentials = Find(userName);
      if (!credentials.Validate()) return false;
      if (credentials.Equals(default) || credentials.Equals(null)) return false;

      if (!_credentials.Remove(credentials)) return false;
      var credentialsString = ToString(credentials);
      Files.DeleteLine(CredentialsFile, credentialsString);
      return true;
    }

   public static string GetUserNames()
    {
      var sb = new StringBuilder();
      foreach (var record in _credentials)
      {
        sb.Append(record.UserName);
        sb.Append(FieldDelimiter);
      }

      if (sb.Length - 1 >= 0 && sb.Length > sb.Length - 1)
      {
        sb.Remove(sb.Length - 1, 1);
      }
      return sb.ToString();
    }

    private static bool Find(Credentials? credentials)
    {
      foreach (var storedCredential in _credentials)
      {
        if (storedCredential.CompareTo(credentials) == 0)
        {
          return true;
        }
      }

      return false;
    }

    private static Credentials Find(string userName)
    {
      foreach (var credential in _credentials)
      {
        if (credential.UserName.Equals(userName))
        {
          return credential;
        }
      }

      return default;
    }

    private static string ToString(Credentials credentials)
    {
      return $"{credentials.UserName}{FieldDelimiter}{credentials.UserPassword}";
    }
  }
}
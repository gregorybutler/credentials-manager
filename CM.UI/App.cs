using System;
using System.Collections.Generic;
using CM.BL;
using CM.DL;
using CM.DL.FilesExceptions;

namespace CM.UI
{
  public static class App
  {
    private static readonly Menu MainMenu = CreateMenu();

    private const string Header = "Credentials Manager";
    private const string LoginSuccessful = "Welcome";
    private const string FailedLogin = "Invalid credentials!";
    private const string RegisterSuccessful = "Registration successful!";
    private const string FailedRegister = "User name is taken!";
    private const string InvalidOption = "Not a valid option!";
    private const string DeleteSuccessful = "User was deleted!";
    private const string FailedDelete = "Unable to delete";

    public static void Run()
    {
      while (true)
      {
        try
        {
          Manager.Initialize();
          PrintMenu();
          ExecuteUserRequest();
        }
        catch (UsersNotFoundException ex)
        {
          Console.WriteLine(ex.Message);
          Pause("Press ENTER to exit..");
          break;
        }
        catch (DuplicateUserCredentialsException ex)
        {
          Console.WriteLine(ex.Message);
          Pause("Press ENTER to exit..");
          break;
        }
      }
    }

    private static IList<MenuItem> GetMenuItems()
    {
      return new List<MenuItem>
      {
        new MenuItem ("1", "Login", Login,
          new List<ConsoleKey> {ConsoleKey.D1, ConsoleKey.NumPad1} ),
        new MenuItem ("2", "Register", Register,
          new List<ConsoleKey> {ConsoleKey.D2, ConsoleKey.NumPad2} ),
        new MenuItem("3", "Delete User", Delete,
          new List<ConsoleKey> {ConsoleKey.D3, ConsoleKey.NumPad3}),
        new MenuItem("4", "List Users", List, 
          new List<ConsoleKey> { ConsoleKey.D4, ConsoleKey.NumPad4 })
      };
    }

    private static Menu CreateMenu()
    {
      return new Menu(Header, GetMenuItems(), new MenuItem("Q", "Exit", Exit,
        new List<ConsoleKey> { ConsoleKey.Q }));
    }

    private static void ConsoleInit(string? header)
    {
      Console.CursorVisible = false;
      Console.Clear();
      Console.WriteLine(header);
      Console.WriteLine();
    }

    private static void PrintMenu()
    {
      ConsoleInit(Header);

      Console.WriteLine(MainMenu.ToString());
    }

    private static void ExecuteUserRequest()
    {
      var isExitInvoked = true;
      do
      {
        var pressedKey = Console.ReadKey(true).Key;
        isExitInvoked = MainMenu.ExitItem.ConsoleKeys.Contains(pressedKey);

        if (MainMenu[pressedKey] == null)
        {
          Console.WriteLine(InvalidOption);
          Pause();
          break;
        }

        ConsoleInit(MainMenu[pressedKey]?.Label);
        MainMenu[pressedKey]?.Action?.Invoke();
        Pause();
        return;
      } while (isExitInvoked);
    }

    private static void Login()
    {
      var userName = PromptString("Username: ");
      var userPassword = PromptString("Password: ");

      var credentials = new Credentials(userName, userPassword);
      var isLoginSuccessful = Manager.Login(credentials);
      var statusMessage = isLoginSuccessful ? $"{LoginSuccessful} {credentials.UserName}!" : FailedLogin;

      Console.WriteLine(statusMessage);
    }

    private static void Register()
    {
      var userName = PromptString("Username: ");
      var userPassword = PromptString("Password: ");

      var isRegisterSuccessful = Manager.Register(new Credentials(userName, userPassword));
      var statusMessage = isRegisterSuccessful ? RegisterSuccessful : FailedRegister;
      Console.WriteLine(statusMessage);
    }

    private static void Delete()
    {
      var userName = PromptString("Username to delete:");
      var isDeleteSuccessful = Manager.Delete(userName);

      var statusMessage = isDeleteSuccessful ? DeleteSuccessful : FailedDelete;
      Console.WriteLine(statusMessage);
    }

    private static void List()
    {
      Console.WriteLine(Manager.GetUserNames());
    }

    private static void Exit()
    {
      Environment.Exit(0);
    }

    private static string PromptString(string message)
    {
      string? userInput;
      do
      {
        Console.Write(message);
        userInput = Console.ReadLine();
      } while (string.IsNullOrWhiteSpace(userInput));

      return userInput;
    }

    private static void Pause(string message = "Press ENTER to continue...")
    {
      Console.WriteLine(message);
      Console.ReadLine();
    }
  }
}
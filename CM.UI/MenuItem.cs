using System;
using System.Collections.Generic;

namespace CM.UI
{
  public class MenuItem
  {
    public string ItemId { get; }
    public string? Label { get; }
    public Action? Action { get; }
    public IList<ConsoleKey> ConsoleKeys { get; }

    public MenuItem(string itemId, string label, Action action, IList<ConsoleKey> consoleKeys)
    {
      ItemId = itemId;
      Label = label;
      Action = action;
      ConsoleKeys = consoleKeys;
    }

    public override string ToString()
    {
      return $"{ItemId}. {Label}";
    }
  }
}
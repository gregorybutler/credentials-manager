using System;
using System.Collections.Generic;
using CM.UI;
using FluentAssertions;
using Xunit;

namespace Tests.CM.UI.Tests
{
  public static class MenuItemTests
  {
    public class ToStringMethod
    {
      [Fact]
      public void Should_Return_String_In_Expected_Format()
      {
        // Arrange
        const string menuItemId = "1";
        const string menuItemLabel = "Test";
        var expectedItem = new MenuItem(menuItemId, menuItemLabel, Console.WriteLine,
          new List<ConsoleKey>() { ConsoleKey.D0 });
        var expectedString = $"{menuItemId}. {menuItemLabel}";

        // Act
        var actual = expectedItem.ToString();

        // Assert
        actual.Should().Be(expectedString);
      }
    }
  }
}
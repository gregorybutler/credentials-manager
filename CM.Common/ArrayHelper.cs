using System;

namespace CM.Common
{
  public static class ArrayHelper
  {
    /// <summary>
    ///   Removes an element from the specified index of the array.
    /// </summary>
    /// <param name="input">The array from which to remove the element.</param>
    /// <param name="index">The position at which we remove the element from the array.</param>
    /// <returns>A new array with the size decreased by 1 and without the element found at specified index.</returns>
    /// <exception cref="ArgumentNullException">Array is not initialized.</exception>
    /// <exception cref="ArgumentException">Array has no elements.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Index is outside the bounds of the array.</exception>
    public static T[] RemoveAt<T>(T[] input, int index)
    {
      if (input == null) throw new ArgumentNullException(nameof(input));
      if (input.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(input));
      if (index < 0 || index >= input.Length) throw new ArgumentOutOfRangeException(nameof(index));

      var newSize = input.Length - 1;
      var output = new T[newSize];
      for (var i = 0; i < output.Length; i++)
      {
        output[i] = i < index ? input[i] : input[i + 1];
      }

      return output;
    }

    /// <summary>
    ///   Inserts a new element in an array at the specified position.
    /// </summary>
    /// <param name="input">Source array.</param>
    /// <param name="element">Element to insert in the array.</param>
    /// <param name="index">Position at which to insert the new element.</param>
    /// <returns>A new array with it's size increased by one and a new element located at the specified index.</returns>
    /// <exception cref="ArgumentNullException">The source array is not initialized.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Index is outside the bounds of the array.</exception>
    public static T[] InsertAt<T>(T[] input, T element, int index)
    {
      if (input == null) throw new ArgumentNullException(nameof(input));
      if (index < 0 || index > input.Length) throw new ArgumentOutOfRangeException(nameof(index));

      var output = new T[input.Length + 1];
      output[index] = element;

      for (var i = 0; i < input.Length; i++)
        if (i < index)
          output[i] = input[i];
        else
          output[i + 1] = input[i];

      return output;
    }

    /// <summary>
    ///   Gets the index of the element.
    /// </summary>
    /// <param name="input">The array where to search the element.</param>
    /// <param name="element">The element which we are getting the index for.</param>
    /// <returns>The index at which the search element was found.</returns>
    /// <exception cref="ArgumentNullException">The source array is not initialized.</exception>
    public static int GetIndex<T>(T[] input, T element)
    {
      if (input == null) throw new ArgumentNullException(nameof(input));

      var index = -1;
      for (var i = 0; i < input.Length; i++)
      {
        if (!Equals(input[i], element)) continue;
        index = i;
        break;
      }

      return index;
    }
  }
}
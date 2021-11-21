using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Extensions
{
  /// <summary>
  /// Extensions for all objects which are sequences
  /// </summary>
  public static class ICustomEnumerableExtension
  {

    /// <typeparam name="TElement">Type of an element in the sequence</typeparam>
    /// <param name="sequence">Sequence to get string from</param>
    /// <param name="ElementsPerLine">How many elements are in one line of the sequence</param>
    /// <returns>
    /// Returns one textuell representation of an sequence
    /// </returns>
    /// <example>
    /// For an array new int[] { 1, 2, 3, 4, 5 } it returns [ 1, 2, 3, 4, 5 ]
    /// </example>
    public static string GetValuesAsString<TElement>(this IEnumerable<TElement> sequence, int ElementsPerLine = 20)
    {
      const int START_COUNTER_VALUE = 1;
      ElementsPerLine = Math.Max(1, Mathf.Abs(ElementsPerLine));
      var result = new StringBuilder("[ ");
      int counter = START_COUNTER_VALUE;
      bool atLeastOneElement = false;

      foreach (TElement element in sequence)
      {
        atLeastOneElement = true;
        result.Append($"{element.ToString()}, ");

        if (counter >= ElementsPerLine)
        {
          result.AppendLine();
          ResetCounter();
        }
        else
        {
          counter++;
        }        
      }

      if (atLeastOneElement)
      {
        // if after the last element is new line appended Environment.NewLine.Length more chars must be removed
        int lengthToRemove = counter == START_COUNTER_VALUE ? (Environment.NewLine.Length + 2) : 2;
        result.Remove(result.Length - lengthToRemove, lengthToRemove);
        result.Append(" ");
      }
      

      result.Append("]");

      return result.ToString();

      void ResetCounter() => counter = START_COUNTER_VALUE;
    }
  } 
}
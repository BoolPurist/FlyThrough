using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NiceGraphicLibrary.Utility
{
  /// <summary>
  /// Instance to pick random elements of a sequence in a way that random element is not returned again 
  /// until all elements of given sequence are returned.
  /// </summary>
  /// <typeparam name="TElement">
  /// Type of a random element of the sequence to pick from
  /// </typeparam>
  public class NoRepeatedRandomPicker<TElement>
  {
#pragma warning disable IDE0090 // Use 'new(...)'
    private List<TElement> _innerCollection = new List<TElement>();
#pragma warning restore IDE0090 // Use 'new(...)'
    private int _currentIndex = -1;

    /// <inheritdoc cref="SetANew(IEnumerable{TElement})" />
    public NoRepeatedRandomPicker(IEnumerable<TElement> collectionToPickFrom)
    {
      SetANew(collectionToPickFrom);
    }

    /// <summary>
    /// Returns a random value from the a given sequence. 
    /// The returned random value does not come from are already given index until all elements of the sequence were returned
    /// </summary>
    /// <returns>
    /// Returns a random value. 
    /// Note: if the given sequence is empty or null then default value of the type of an element is returned
    /// </returns>

    public TElement Next
    {
      get
      {
        if (_innerCollection.Count == 0)
        {
          return default;
        }
        else
        {          
          if (_currentIndex == _innerCollection.Count)
          {
            Reset();
          }

          TElement nextElement = _innerCollection[_currentIndex];
          _currentIndex++;

          return nextElement;
        }
      }
    }

    /// <summary>
    /// Resets the given sequence so the random elements are picked as if the no element was picked before.
    /// </summary>
    public void Reset()
    {
      RandomUtility.Shuffle(_innerCollection);
      _currentIndex = 0;
    }

    /// <summary>
    /// Changes given sequence to choose random elements from.
    /// A copy of the given sequence is created as inner sequence for picking.
    /// </summary>
    /// <param name="collectionToPickFrom">
    /// New given sequence for picking random elements.
    /// If null then the old given sequence is kept for picking
    /// </param>
    public void SetANew(IEnumerable<TElement> collectionToPickFrom)
    {
      if (collectionToPickFrom != null)
      {
        _innerCollection = new List<TElement>(collectionToPickFrom);
        Reset();
      }
    }
  } 
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Utility
{
  /// <summary>
  /// Collection of functions for working with randomness in a game.
  /// </summary>
  public static class RandomUtility
  {

    private static IRandomGenerator _randomGenerator = new UnityRandomGenerator();

    /// <summary>
    /// Changes implementation of generating randomness of the functions under this class.
    /// </summary>
    /// <param name="newRandomGenerator">
    /// New implementation to use for generating randomness
    /// If null the old implementation is kept
    /// </param>
    public static void SetRandomGenerator(IRandomGenerator newRandomGenerator)
    {
      if (newRandomGenerator != null)
      {
        _randomGenerator = newRandomGenerator;
      }
    }

    /// <summary>
    /// Chooses element from the sequence randomly. Every element has the same chance to be picked.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the returned value and of the elements of the given sequence
    /// </typeparam>
    /// <param name="listToPrikFrom">
    /// Sequence to choose a random element from.
    /// If it is null or empty, the default value of the type of the an element is returned
    /// </param>
    /// <returns>
    /// Returns randomly chosen element
    /// </returns>
    public static TElement PickRandomFrom<TElement>(IList<TElement> listToPrikFrom)
    {
      if (listToPrikFrom == null || listToPrikFrom.Count == 0)
      {
        return default;
      }

      return listToPrikFrom[_randomGenerator.Range(0, listToPrikFrom.Count - 1)];
    }

    /// <summary>
    /// Checks if a chance has occurred.
    /// </summary>
    /// <param name="probability">
    /// Threshold to reach for a chance to occur.
    /// </param>
    /// <param name="total">
    /// Maximum value possible to which the Threshold is relative to.
    /// </param>
    /// <returns>
    /// Returns true if the chance occurs.
    /// </returns>
    /// <example>
    /// We want to now if chance occurs with 50 percent
    /// <code>
    /// bool returnValue = DoesChanceOccure(0.5f);
    /// // if returnValue is true then the chance with 50 percent has occurred.
    /// </code>
    /// We want to now if chance occurs with 80 percent from 200 percent as total.
    /// <code>
    /// bool returnValue = DoesChanceOccure(0.8f, 2f);
    /// // if returnValue is true then the chance with 80 percent has occurred from 200 percent as total.
    /// </code>    
    /// </example>
    public static bool DoesChanceOccur(float probability, float total = 1f)
    {
      total = Mathf.Abs(total);
      probability = Mathf.Abs(probability);
      probability = Math.Min(probability, total);
      float probabilityThreshold = _randomGenerator.Value * total;
      return probabilityThreshold <= probability;
    }

    /// <summary>
    /// Take a sequence of given chances and returns first smallest chance occurred. 
    /// </summary>
    /// <param name="list">
    /// Given sequence to choose the index from
    /// </param>
    /// <returns>
    /// Randomly chosen index from the given sequence
    /// </returns>
    public static int ChooseByChances(IList<float> list)
    {
      float total = 0f;
      var chances = new List<float>();

      foreach (float chance in list)
      {
        float absoluteChance = Mathf.Abs(chance);
        chances.Add(absoluteChance);
        total += absoluteChance;
      }

      chances.Sort();

      float randomPoint = _randomGenerator.Value * total;

      for (int i = 0; i < chances.Count; i++)
      {
        float randomChance = chances[i];
        if (randomPoint < randomChance)
        {
          return i;
        }
        else
        {
          randomPoint -= randomChance;
        }

      }

      return chances.Count - 1;
    }

    /// <summary>
    /// Shuffles the give sequence so elements are in new random order.
    /// </summary>
    public static void Shuffle<TElement>(IList<TElement> list)
    {
      if (list == null)
      {
        return;
      }
      else
      {
        for (int i = 0; i < list.Count; i++)
        {
          TElement temp = list[i];
          int randomIndex = _randomGenerator.Range(0, list.Count);
          list[i] = list[randomIndex];
          list[randomIndex] = temp;
        }
      }
    }
  }

  
}
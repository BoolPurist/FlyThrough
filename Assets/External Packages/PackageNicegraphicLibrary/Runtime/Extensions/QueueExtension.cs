using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace NiceGraphicLibrary.Extensions
{
  public static class QueueExtension
  {
    public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> sequenceToAppend)
    {
      if (sequenceToAppend != null)
      {
        foreach (T newElementToAppend in sequenceToAppend)
        {
          queue.Enqueue(newElementToAppend);
        }
      }
    }
  } 
}
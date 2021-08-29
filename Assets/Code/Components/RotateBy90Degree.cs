using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlyThrough
{
  public enum RotateDirection { Left, Right, Up, Down, Forward, Back }
  public enum RotaionStep { One, Two, Three }

  [ExecuteInEditMode]
  [RequireComponent(typeof(Renderer))]
  public class RotateBy90Degree : MonoBehaviour
  {

    private const int MAX_RANDOM_NEXT = 4;

#pragma warning disable IDE0066

    private Vector3 RotationAxis
    {
      get
      {
        switch (_rotateDirection)
        {
          case RotateDirection.Left:
            return Vector3.left;
          case RotateDirection.Right:
            return Vector3.right;
          case RotateDirection.Up:
            return Vector3.up;
          case RotateDirection.Down:
            return Vector3.down;
          case RotateDirection.Forward:
            return Vector3.forward;
          case RotateDirection.Back:
            return Vector3.back;
          default:
            return Vector3.zero;
        }
      }
    }

    private float RoationAmount
    {
      get
      {
        switch (_rotationStep)
        {
          case RotaionStep.One:
            return 90f;
          case RotaionStep.Two:
            return 180f;
          case RotaionStep.Three:
            return 270f;
          default:
            return 0f;
        }
      }
    }

#pragma warning restore IDE0066

    [SerializeField]
    private RotateDirection _rotateDirection = RotateDirection.Forward;  
    [SerializeField]
    private RotaionStep _rotationStep = RotaionStep.One;

#pragma warning disable IDE0090 // Use 'new(...)'
    private readonly System.Random _randomGenerator = new System.Random();
#pragma warning restore IDE0090 // Use 'new(...)'

    public RotateDirection Direction
    {
      get => _rotateDirection;
      set => _rotateDirection = value;
    }

    public RotaionStep RotaionStep
    {
      get => _rotationStep;
      set => _rotationStep = value;
    }

    private int RandomRotationAmount => _randomGenerator.Next(0, MAX_RANDOM_NEXT) * 90;

    [ContextMenu("Rotate")]
    public void Rotate() => transform.RotateAround(transform.position, RotationAxis, RoationAmount);
    [ContextMenu("Random steps in rotation")]
    public void RotateRandomSteps() => transform.RotateAround(transform.position, RotationAxis, RandomRotationAmount);

    }
}

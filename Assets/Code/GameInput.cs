using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace FlyThrough
{
  public abstract class GameInput : MonoBehaviour
  {
    public event Action<Vector2> OnMovementXYInput;
    public event Action<float> OnScaleYInput;
    public event Action<float> OnRotationZInput;
    public event Action OnPauseInput;
    public event Action<float> OnPushManuelly;


    protected virtual void InvokeOnMovementXYInput(Vector2 eventArg)
      => OnMovementXYInput?.Invoke(eventArg);

    protected virtual void InvokeOnScaleYInput(float eventArg)
      => OnScaleYInput?.Invoke(eventArg);

    protected virtual void InvokeOnRotationZInputt(float eventArg)
      => OnRotationZInput?.Invoke(eventArg);

    protected virtual void InvokeOnPushManuelly(float eventArg)
      => OnPushManuelly?.Invoke(eventArg);

    protected virtual void InvokeOnPauseInput() => OnPauseInput?.Invoke();
  }
}

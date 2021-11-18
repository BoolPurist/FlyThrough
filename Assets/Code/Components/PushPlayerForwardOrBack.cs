using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace FlyThrough
{

  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(PlayerInput))]
  public class PushPlayerForwardOrBack : MonoBehaviour
  {
    [SerializeField]
    [Min(0)]
    private float Speed = 100f;
    [SerializeField]
    [Min(0)]
    private float _travelDistanceThreshold = 10f;
    [SerializeField]
    private bool _manuelControl = false;
    [SerializeField]
    [Min(0)]
    private float SpeedAfterDeath = 10f;

    


    public Action<float> OnTraveledThreshold;

    private GameInput _input;


    private float _pushInput;

    private void ResetInputs() => _pushInput = 0f;

    private Vector3 currentReferencLocation;

    private Rigidbody _rb;

    private void Start()
    {
      _rb = GetComponent<Rigidbody>();
      currentReferencLocation = transform.position;
    }

    private void OnEnable()
    {
      StartListeningForInputs();
      ResetInputs();
    }

    private void OnDisable()
      => StopListeningForInputs();

    private void OnDestroy()
      => StopListeningForInputs();

    private void OnPushInput(float input)
    {
      if (_manuelControl)
      {
        _pushInput = Mathf.Clamp(_pushInput + input, -1f, 1f);
      }
    }

    public bool SetManuelControl(bool controlManuelly) => _manuelControl = controlManuelly;

    private void StartListeningForInputs()
    {
      _input = FindObjectOfType<GameInput>();

      if (_input == null)
      {
        Debug.LogWarning(
          $"No component {typeof(GameInput).Name} found for listening to inputs in the scene"
          );
      }

      _input.OnPushManuelly += OnPushInput;
    }

    private void StopListeningForInputs()
    {
      if (_input != null)
      {
        _input.OnPushManuelly -= OnPushInput;
      }
    }

    private void FixedUpdate()
    {
      _pushInput = _manuelControl ? _pushInput : 1f;
      float currentSpeed = (_pushInput * Speed) * Time.deltaTime;
      Vector3 movement = Vector3.forward * currentSpeed;
      _rb.AddForce(movement, ForceMode.VelocityChange);

      float distance = Vector3.Distance(transform.position, currentReferencLocation);

      if (distance >= _travelDistanceThreshold)
      {
        OnTraveledThreshold?.Invoke(_travelDistanceThreshold);
        currentReferencLocation = transform.position;
      }
      
      ResetInputs();
    }

    public void InitLastMovesToGameOver()
    {
      Speed = SpeedAfterDeath;
    }
  }

}
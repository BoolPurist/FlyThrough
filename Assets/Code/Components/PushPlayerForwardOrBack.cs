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
    private float TravelDistanceThreshold = 10f;
    [SerializeField]
    private bool _manuelControl = false;
    [SerializeField]
    [Min(0)]
    private float SpeedAfterDeath = 10f;

    [SerializeField]
    private UnityEvent OnTraveledThreshold;
    



    private Vector3 currentReferencLocation;
    
    private Rigidbody _rb;
    private PlayerInput _input;

    // Start is called before the first frame update
    void Start()
    {
      _rb = GetComponent<Rigidbody>();
      _input = GetComponent<PlayerInput>();
      currentReferencLocation = transform.position;
    }

    

    private void FixedUpdate()
    {
      bool movedForward = !_manuelControl || _input.MoveForwardPressed;
      if ( movedForward || _input.MoveBackwardPressed )
      {
        Vector3 direction = movedForward ? Vector3.forward : Vector3.back;
        Vector3 movement = (direction * Speed) * Time.deltaTime;
        _rb.AddForce(movement, ForceMode.VelocityChange);


        float distance = Vector3.Distance(transform.position, currentReferencLocation);

        if (distance >= TravelDistanceThreshold)
        {
          OnTraveledThreshold.Invoke();
          currentReferencLocation = transform.position;
        }
      }

      
    }

    public void InitLastMovesToGameOver()
    {
      Speed = SpeedAfterDeath;
    }
  }

}
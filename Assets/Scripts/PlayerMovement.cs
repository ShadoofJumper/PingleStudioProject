using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SpeedParams
{
    public float walk;
    public float run;
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private SpeedParams _speed;

    private Rigidbody _characterRig;
    private Plane _virtualPlane;
    private float _currentSpeedMod;
    
    private void Start()
    {
        _characterRig = GetComponent<Rigidbody>();
        _virtualPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
    }
    
    
    
    private void FixedUpdate()
    {
        //update move
        if (Input.GetKey(KeyCode.LeftShift)) {
            _currentSpeedMod = _speed.run;
        }
        else {
            _currentSpeedMod = _speed.walk;
        }
        
        Vector3 moveVelocity = _playerInput.Velocity * _currentSpeedMod * _currentSpeedMod * Time.fixedDeltaTime;
        Vector3 newPos = _characterRig.transform.position + moveVelocity;
        _characterRig.MovePosition(newPos);
        //update rotation
        Vector3 lookPoint = GetLookPoint();
        Quaternion newRotateion = Quaternion.LookRotation(lookPoint);
        _characterRig.MoveRotation(newRotateion);
    }

    private Vector3 GetLookPoint()
    {
        Vector3 pointToLook = new Vector3();
        float enter = 0.0f;
        Ray ray = Camera.main.ScreenPointToRay(_playerInput.MousePosition);
        _virtualPlane.distance = transform.position.y;
        if (_virtualPlane.Raycast(ray, out enter))
        {
            pointToLook = ray.GetPoint(enter);
        }
        Vector3 lookVector = Vector3.Normalize(pointToLook - transform.position);
        lookVector.y = 0.0f;
        return lookVector;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawSphere(__pointToLook,0.5f);
    //}
}

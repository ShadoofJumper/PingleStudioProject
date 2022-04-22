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
    [SerializeField] private Animator _characterAnimator; 
    
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
        UpdateMove();
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        Vector3 lookPoint = GetLookPoint();
        Quaternion newRotateion = Quaternion.LookRotation(lookPoint);
        _characterRig.MoveRotation(newRotateion);
    }
    
    private void UpdateMove()
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
            _currentSpeedMod = _speed.run;
        else
            _currentSpeedMod = _speed.walk;
        
        Vector3 moveVelocity = _playerInput.RawVelocity * _currentSpeedMod * Time.fixedDeltaTime;
        Vector3 newPos = _characterRig.transform.position + moveVelocity;
        _characterRig.MovePosition(newPos);

        //update animation move
        Vector3 pos         = _playerInput.Velocity * _currentSpeedMod / _speed.run;
        Vector3 localPos    = _characterRig.transform.InverseTransformDirection(pos);
        float objectSpeed   = pos.magnitude;

        bool rawStopAnim = moveVelocity.magnitude < 0.1f;
        UpdateMoveAnim(objectSpeed, localPos, rawStopAnim);
    }
    
    public void UpdateMoveAnim(float currentSpeed, Vector3 movePos, bool playerRawStop = false)
    {
        if (playerRawStop)
            currentSpeed = 0;
        _characterAnimator.SetFloat("Speed", currentSpeed);
        _characterAnimator.SetFloat("PosX", movePos.x);
        _characterAnimator.SetFloat("PosY", movePos.z);
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
    
}

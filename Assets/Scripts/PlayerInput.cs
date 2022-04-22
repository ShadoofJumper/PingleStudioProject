using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Vector3 _velocity;
    private Vector3 _rawVelocity;
    private Vector3 _mousePosition;
    
    public Vector3 Velocity => _velocity;
    public Vector3 RawVelocity => _rawVelocity;

    public Vector3 MousePosition => _mousePosition;
    public Action OnLeftMouseButton;
    public Action OnRightMouseButton;

    void Update()
    {
        if (_player.IsBlocked)
            return;
        
        TrackClicks();
        TrackVelocity();
        TrackMousePosition();
    }

    private void TrackMousePosition()
    {
        _mousePosition = Input.mousePosition;
    }
    
    private void TrackVelocity()
    {
        Vector3 verticalMovement    = Vector3.forward   * Input.GetAxis("Vertical");
        Vector3 horizontalMovement  = Vector3.right     * Input.GetAxis("Horizontal");
        
        Vector3 verticalRawMovement    = Vector3.forward   * Input.GetAxisRaw("Vertical");
        Vector3 horizontalRawMovement  = Vector3.right     * Input.GetAxisRaw("Horizontal");

        _velocity = verticalMovement + horizontalMovement;
        _velocity = _velocity.normalized;

        _rawVelocity = verticalRawMovement + horizontalRawMovement;
        _rawVelocity = _rawVelocity.normalized;
    }
    
    private void TrackClicks()
    {
        if (Input.GetMouseButtonDown(0))
            OnLeftMouseButton?.Invoke();
        
        if (Input.GetMouseButtonDown(1))
            OnRightMouseButton?.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Transform _cameraMoveTransform;
    [SerializeField] private Transform _cameraRotateTransform;
    
    [SerializeField] private float _zoomSensitivity = 30;
    [SerializeField] private float _zoomSensitivityLerm = 30;
    
    [SerializeField] private float _zoomMin = 3;
    [SerializeField] private float _zoomMax = 5;

    [SerializeField] private float _moveSmoothens = 1f;

    private bool _haveTargetToFollow = false;
    private Vector3 _startOffsetCamera;
    private float _startLocalAxesZ;
    
    private void Start()
    {
        _haveTargetToFollow = _followTarget != null;
        _startOffsetCamera = _cameraMoveTransform.position - _followTarget.position;
        _startOffsetCamera.y = 0;
        _startLocalAxesZ = _cameraRotateTransform.localPosition.z;
    }

    private void FixedUpdate()
    {
        FollowTarget();
        TargetZoom();
    }

    private void FollowTarget()
    {
        if (!_haveTargetToFollow && _startOffsetCamera != null)
            return;

        Vector3 targetPosition = _followTarget.position;
        targetPosition.y = _cameraMoveTransform.position.y;
        targetPosition += _startOffsetCamera;
        Vector3 newCameraPos = Vector3.Lerp(_cameraMoveTransform.position, targetPosition, _moveSmoothens * Time.fixedDeltaTime);
        _cameraMoveTransform.position = newCameraPos;
    }

    private float targetFov;
    private void TargetZoom()
    {
        float fov = targetFov == 0 ? Camera.main.fieldOfView : targetFov;
        fov += Input.GetAxis("Mouse ScrollWheel") * _zoomSensitivity;
        fov = Mathf.Clamp(fov, _zoomMin, _zoomMax);
        targetFov = fov;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, _zoomSensitivityLerm *Time.fixedDeltaTime);
        
        //Camera.main.fieldOfView = targetFov;
    }
    
}

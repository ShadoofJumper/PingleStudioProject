using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveSmoothens = 1f;
    
    private bool _haveTargetToFollow = false;
    private Vector3 _startOffsetCamera;

    private void Start()
    {
        _haveTargetToFollow = _followTarget != null;
        _startOffsetCamera = _camera.position - _followTarget.position;
        _startOffsetCamera.y = 0;
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (!_haveTargetToFollow && _startOffsetCamera != null)
            return;

        Vector3 targetPosition = _followTarget.position;
        targetPosition.y = _camera.position.y;
        targetPosition += _startOffsetCamera;
        Vector3 newCameraPos = Vector3.Lerp(_camera.position, targetPosition, _moveSmoothens * Time.fixedDeltaTime);
        _camera.position = newCameraPos;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinEffectLogic : MonoBehaviour
{
    [SerializeField] private ParticleSystem _coinMeshParticle;
    private Transform _target;
    private bool _isMove;
    private float _moveTime;
    float _currentLerpTime;
    private Vector3 _startPos;
    
    public void StartMoveToTarget(Transform target, float moveTime)
    {
        _startPos = transform.position;
        _moveTime = moveTime;
        _target = target;
        _isMove = true;
    }

    private void Update()
    {
        if (!_isMove)
            return;
        

        _currentLerpTime += Time.deltaTime;
        if (_currentLerpTime > _moveTime) {
            _currentLerpTime = _moveTime;
        }
 
        //move to player
        float t = _currentLerpTime / _moveTime;
        t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
        transform.position = Vector3.Lerp(transform.position, _target.position, t);
        //add move distorion
        transform.position += Vector3.up * 0.05f * (1-t);

        if (Vector3.Distance(transform.position, _target.position) < 0.1f)
        {
            Destroy(_coinMeshParticle);
            _isMove = false;
            DOVirtual.DelayedCall(1.5f, () => { Destroy(gameObject); });
        }
    }
}

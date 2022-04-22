using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private Rigidbody _myRig;
    [SerializeField] private NavMeshAgent _navMeshAgent; 
    [Range(1,10)]
    [SerializeField] private float _searchRange = 10; 
    [Range(0.1f,10.0f)]
    [SerializeField] private float _attackRange = 2;
    [SerializeField] private float _runMoveSpeed = 3;
    [SerializeField] private float _walkMoveSpeed = 2;

    private bool _playerInRange = false; 
    private bool _moveToTargetInProgress = false;
    private bool _playerInRangeAttack = false;
    private bool _attackInProgress = false;
    private Vector3 _moveToPos;
    
    private Tween _attackLoop;
    private Tween _playerHitTween;

    private void Start()
    {
        _navMeshAgent.speed = _runMoveSpeed;
    }

    void Update()
    {
        CheckEnemyNear();
        MoveLogic();
        AttackLogic();
    }

    private void MoveLogic()
    {
        if (_playerInRange)
        {
            MoveToEnemy();
        }
        else
        {
            if(_moveToTargetInProgress)
                StopMove();
        }
    }

    private void AttackLogic()
    {
        if (_playerInRangeAttack)
        {
            RotateToTarget();
            if(!_attackInProgress)
                Attack();
        }
        else
        {
            _attackInProgress = false;
            if (_attackLoop != null) _attackLoop.Kill();
            if(_playerHitTween != null) _playerHitTween.Kill();
        }
    }

    private void RotateToTarget()
    {
        Debug.Log("RotateToTarget");
        Vector3 vectorToTarget = _player.transform.position - transform.position;
        vectorToTarget.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(vectorToTarget);
    }
    
    private void Attack()
    {
        _attackInProgress = true;
        //play anim
        _characterAnimator.SetTrigger("Attack");
        //play next attack
        _attackLoop = DOVirtual.DelayedCall(1.5f, Attack);
        //send hit call
        _playerHitTween = DOVirtual.DelayedCall(0.5f, () =>
        {
            _player.GetComponent<Player>().GetHit();
        });
    }
    
    private void CheckEnemyNear()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 myPos = transform.position;
        //check to move
        _playerInRange = Vector3.Distance(playerPos, myPos) < _searchRange 
                         && Vector3.Distance(playerPos, myPos) >= _attackRange;
        _playerInRangeAttack = Vector3.Distance(playerPos, myPos) < _attackRange;
    }

    private void MoveToEnemy()
    {
        float normilizedSpeed = _navMeshAgent.velocity.magnitude / _runMoveSpeed;
        _characterAnimator.SetFloat("Velocity", normilizedSpeed);
        if (Vector3.Distance(_moveToPos, _player.transform.position) > 0.01f)
        {
            _moveToPos = _player.transform.position;
            _navMeshAgent.SetDestination(_player.transform.position);
            _moveToTargetInProgress = true;
        }

    }

    private void StopMove()
    {
        _characterAnimator.SetFloat("Velocity", 0);
        _navMeshAgent.ResetPath();
        _moveToTargetInProgress = false;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position 
            , Vector3.up
            , _searchRange); 
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position
            , Vector3.up
            , _attackRange); 
    }
}

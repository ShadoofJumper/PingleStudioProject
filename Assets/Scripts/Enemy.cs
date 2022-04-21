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
    [SerializeField] private NavMeshAgent _navMeshAgent; 
    [Range(1,10)]
    [SerializeField] private float _searchRange = 10; 
    [Range(0.1f,10.0f)]
    [SerializeField] private float _attackRange = 2;

    private bool _playerInRange = false; 
    private bool _moveToTargetInProgress = false;
    private bool _playerInRangeAttack = false;
    private bool _attackInProgress = false;
    private Vector3 _moveToPos;
    
    private Tween _attackLoop;
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
            if(!_attackInProgress)
                Attack();
        }
        else
        {
            _attackInProgress = false;
            if (_attackLoop != null)
                _attackLoop.Kill();
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
        _attackInProgress = true;
        //play anim
        _characterAnimator.SetTrigger("Attack");
        //play next attack
        _attackLoop = DOVirtual.DelayedCall(1.5f, Attack);
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
        if (Vector3.Distance(_moveToPos, _player.transform.position) > 0.01f)
        {
            _moveToPos = _player.transform.position;
            Debug.Log("MoveToEnemy");
            _navMeshAgent.SetDestination(_player.transform.position);
            _moveToTargetInProgress = true;
        }

    }

    private void StopMove()
    {
        Debug.Log("StopMove");
        _navMeshAgent.ResetPath();
        _moveToTargetInProgress = false;
    }

    private void OnDrawGizmos()
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

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private GameObject _hitParticle;
    [SerializeField] private Transform _hitAnchor;

    private bool _isBlocked = false;

    public bool IsBlocked => _isBlocked;

    public void GetHit()
    {
        _playerAnim.SetTrigger("GetHit");
        CreateHitParticle();
        _isBlocked = true;
        DOVirtual.DelayedCall(0.5f, () =>
        {
            _isBlocked = false;
        });
    }

    private void CreateHitParticle()
    {
        Instantiate(_hitParticle, _hitAnchor.position, quaternion.identity,_hitAnchor);
    }
}

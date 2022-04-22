using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _playerAnim;
    private bool _isBlocked = false;

    public bool IsBlocked => _isBlocked;

    public void GetHit()
    {
        _playerAnim.SetTrigger("GetHit");
        _isBlocked = true;
        DOVirtual.DelayedCall(0.5f, () =>
        {
            _isBlocked = false;
        });
    }
}

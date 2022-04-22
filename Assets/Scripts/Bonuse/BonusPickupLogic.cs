using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BonusPickupLogic : EffectBase
{
    [SerializeField] private GameObject _bonusPickupImpact;
    [SerializeField] private GameObject _bonusPickupCoin;

    [SerializeField] private int _coinsCount = 3;
    [SerializeField] private float _delayBetweenCoins = 0.2f;
    [SerializeField] private float _coinSpeed = 1.0f;
    [SerializeField] private float _coinJumpSpeed = 0.2f;
    [SerializeField] private Ease _easy;
    
    private Transform _target;
    

    public override void PlayerEffect(Transform target)
    {
        _target = target;
        Instantiate(_bonusPickupImpact, transform.position, Quaternion.identity, transform);
        for (int i = 0; i < _coinsCount; i++)
        {
            DOVirtual.DelayedCall(_delayBetweenCoins*i, ()=>
            {
                SpawnCoin();
            });
        }
    }


    private void SpawnCoin()
    {
        GameObject coin = Instantiate(_bonusPickupCoin, transform.position, Quaternion.identity);
        CoinEffectLogic coinEffectLogic = coin.GetComponent<CoinEffectLogic>();
        Transform coinTransform = coin.transform;
        Vector3 targetJump = transform.position + Vector3.up * 0.5f;
        coinTransform.DOMove(targetJump, _coinJumpSpeed)
            .SetEase(_easy).OnComplete(() =>
            {
                coinEffectLogic.StartMoveToTarget(_target, _coinSpeed); ;
            });
        
    
    }

}

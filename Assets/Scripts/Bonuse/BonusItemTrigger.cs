using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BonusItemTrigger : MonoBehaviour
{
    [SerializeField] private EffectBase _effetcToSpawn;
    
    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.gameObject.GetComponent<Player>();
        if (_player != null)
        {
            ActivateItem(_player);
        }
    }

    protected virtual void ActivateItem(Player target)
    {
        GameObject go = Instantiate(_effetcToSpawn.gameObject, transform.position, Quaternion.identity);
        go.GetComponent<EffectBase>().PlayerEffect(target.transform);
        Destroy(gameObject);
    }
}

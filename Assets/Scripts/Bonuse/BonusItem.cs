using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BonusItem : MonoBehaviour
{
    [SerializeField] private GameObject _effetcToSpawn;
    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.gameObject.GetComponent<Player>();
        if (_player != null)
        {
            ActivateItem();
        }
    }

    protected virtual void ActivateItem()
    {
        Instantiate(_effetcToSpawn, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

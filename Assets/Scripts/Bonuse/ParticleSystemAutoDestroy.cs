using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ParticleSystemAutoDestroy  : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ps;
    public void FixedUpdate()
    {
        if (_ps && !_ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}

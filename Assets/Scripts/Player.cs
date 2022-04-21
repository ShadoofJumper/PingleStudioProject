using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _playerAnim;
    

    public void GetHit()
    {
        Debug.Log("Get hit!");
    }
}

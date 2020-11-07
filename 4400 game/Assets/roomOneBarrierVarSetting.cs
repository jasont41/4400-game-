using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomOneBarrierVarSetting : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.Instance.hasLeftRoomOneForFirstTime = true; 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomOneBarrierVarSetting : MonoBehaviour
{
    public GameObject oldman;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.Instance.hasLeftRoomOneForFirstTime = true;
            if (!checkIfOldManCanGoAway())
            {
                oldman.SetActive(false); 
            }
        }
        
    }
    private bool checkIfOldManCanGoAway()
    {
        if(PlayerMovement.Instance.player_tier <= 2 && PlayerMovement.Instance.hasSpokenToOldMan == true)
        {
            return true; 
        }
        else
        {
            return false; 
        }
    }
}

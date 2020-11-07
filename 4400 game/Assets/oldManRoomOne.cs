using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldManRoomOne : MonoBehaviour
{
    public GameObject oldman; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.Instance.hasSpokenToOldMan = true; 
        }
    }


    public void LateUpdate()
    {
        if(PlayerMovement.Instance.hasSpokenToOldMan == true &&
            PlayerMovement.Instance.hasLeftRoomOneForFirstTime == true)
        {
            oldman.SetActive(false); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderScript : MonoBehaviour
{
    public bool playerInRange;
    public GameObject TraderUI;

    private void Start()
    {
        TraderUI = canvas_dont_destroy.Instance.traderUI;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            Debug.Log("Got here - Trader"); 
            if (TraderUI.activeInHierarchy)
            {
                TraderUI.SetActive(false);
                PlayerMovement.Instance.prevent_movement = true; 
            }
            else
            {
                TraderUI.SetActive(true);
                PlayerMovement.Instance.prevent_movement = false; 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in Range");
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

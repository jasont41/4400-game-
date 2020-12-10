using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderScript : MonoBehaviour
{
    public bool playerInRange;
    public GameObject TraderUI;
    public int potionQuan;
    public int potionTotal;
    public int cartTotal;
    public int potionCost;
    public Text potionCostText;
    public Text potionQuanText; 
    private void Start()
    {
        potionQuan = potionTotal = cartTotal = 0; //zero it all out 
        TraderUI = canvas_dont_destroy.Instance.traderUI;
        potionCost = 50;
        
    }
    private void Update()
    {
        if (TraderUI.activeInHierarchy == true)
        {
             
        }
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
                potionCostText = GameObject.FindGameObjectWithTag("PotionCostText").GetComponent<Text>();
                potionCostText = GameObject.FindGameObjectWithTag("PotionQuantity").GetComponent<Text>();
                potionQuanText.text = potionQuan.ToString(); 
                potionCostText.text = "Cost: $" + potionCost.ToString();
                
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

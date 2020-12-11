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
    public Text potionTotalText; 
    

    private void Start()
    {
        potionQuan = 1;
        potionTotal = potionCost * potionQuan;
        TraderUI = canvas_dont_destroy.Instance.traderUI;
        TraderUI.SetActive(true);
        potionCostText = GameObject.FindGameObjectWithTag("PotionCostText").GetComponent<Text>();
        potionQuanText = GameObject.FindGameObjectWithTag("PotionQuantity").GetComponent<Text>();
        potionTotalText = GameObject.FindGameObjectWithTag("PotionTotal").GetComponent<Text>();
        TraderUI.SetActive(false); 
        potionCost = 50;
        
    }
    private void Update()
    {
        potionTotal = potionCost * potionQuan; 
        if (TraderUI.activeInHierarchy == true)
        {
            potionQuanText.text = potionQuan.ToString();
            potionCostText.text = "Cost: $" + potionCost.ToString();
            potionTotalText.text = "$" + potionTotal.ToString();
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
                
                PlayerMovement.Instance.prevent_movement = false; 
            }
           
        }
        
    }

    public void increasePotion()
    {
        if (PlayerMovement.Instance.player_money >= (potionQuan * potionCost))
        {
            potionQuan++;
        }
    }

    public void decreasePotion()
    {
        if (potionQuan > 0)
        {
            potionQuan--;
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

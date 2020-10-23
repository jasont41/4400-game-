using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class Sign : MonoBehaviour
{
   // public GameObject dialogBox;
    //public Text dialogText;
    public string dialog;
    public bool playerInRange; 

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (canvas_dont_destroy.Instance.dialogBox.activeInHierarchy)
            {
                canvas_dont_destroy.Instance.dialogBox.SetActive(false); 
            }
            else
            {
                canvas_dont_destroy.Instance.dialogBox.SetActive(true);
                canvas_dont_destroy.Instance.dialogBox_text.text = dialog; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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

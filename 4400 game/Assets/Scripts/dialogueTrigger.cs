using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI; 



[System.Serializable]
public class dialogueTrigger : MonoBehaviour
{
    public bool playerInRange;
    public GameObject dialogue_box;
    public Text dialogue_text;
    int sentence_index = 0; 
    public string[] sentences;  
    
    //public Queue<string> sentences; 


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            
            if (dialogue_box.activeInHierarchy)
            {
                dialogue_box.SetActive(false); 
            }
            else
            {
                dialogue_text.text = sentences[0];
                PlayerMovement.Instance.addNormalPotion(); 
                dialogue_box.SetActive(true);
                displayDialogue(); 
            }
        }
    }


    private void displayDialogue()
    {
        if (sentence_index == sentences.Length)
        {
            dialogue_box.SetActive(false); 
            return;
        }
        else
        {
            dialogue_text.text = sentences[sentence_index];
        }
    }
    public void nextSentence()
    {
        sentence_index++;
        displayDialogue();

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

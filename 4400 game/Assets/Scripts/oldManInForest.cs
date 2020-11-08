using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


[System.Serializable]
public class oldManInForest : MonoBehaviour
{
    public Vector3 spawnLocation; 
    public GameObject oldMan; 
    public bool playerInRange;
    public GameObject dialogue_box;
    public Text dialogue_text;
    int sentence_index = 0;
    public string[] sentences;


    public string NewScene; 
    //public Queue<string> sentences; 


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            nextSentence();
        }

    }


    private void displayDialogue()
    {
        if (sentence_index == sentences.Length)
        {
            PlayerMovement.Instance.enemyPrefab = oldMan;
            PlayerMovement.Instance.setPOS(transform.position);
            PlayerMovement.Instance.transform.position = spawnLocation;
            SceneManager.LoadScene(NewScene);
            PlayerMovement.Instance.getPOS();
            PlayerMovement.Instance.prevent_movement = false; // prevents player from walking around the scene 
            PlayerMovement.Instance.setMovingFalse(); //stops player from moving in place
            Debug.Log("it works ");
            Debug.Log(PlayerMovement.Instance.getPOS());
            
            
            //SceneManager.LoadScene(NewScene); 
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

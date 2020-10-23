using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class canvas_dont_destroy : MonoBehaviour
{
    //Have to make the whole thing a singleton or else the UI breaks after changing scenes 
  
    public GameObject dialogBox;
    public GameObject stats_UI;
    public Text dialogBox_text;
    public Text stats_UI_text;
    public GameObject roomTransferText; 
    public static canvas_dont_destroy Instance { get; private set; }
    private void Awake()
    {
        dialogBox = GameObject.FindGameObjectWithTag("dialog_box");
        dialogBox_text = GameObject.FindGameObjectWithTag("dialog_box_text").GetComponent<Text>();
        stats_UI = GameObject.FindGameObjectWithTag("stats_UI");
        stats_UI_text = GameObject.FindGameObjectWithTag("stats_UI_text").GetComponent<Text>();
       

        dialogBox.SetActive(false);
        stats_UI.SetActive(false);
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }







}

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
    public GameObject roomTransferText_theObject; 
    public Text roomTransferText;
    public GameObject exp_bar;
    public Text tier_text;
    public Text exp_text;
    public Text attack_damage_text;
    public GameObject HealPotionIcon; 

    public static canvas_dont_destroy Instance { get; private set; }
    private void Awake()
    {
        HealPotionIcon = GameObject.FindGameObjectWithTag("healPotionIcon");
        if (HealPotionIcon == null) { Debug.Log("erroed out");  }
        exp_bar = GameObject.FindGameObjectWithTag("exp_bar"); 
        dialogBox = GameObject.FindGameObjectWithTag("dialog_box");
        dialogBox_text = GameObject.FindGameObjectWithTag("dialog_box_text").GetComponent<Text>();
        stats_UI = GameObject.FindGameObjectWithTag("stats_UI");
        stats_UI_text = GameObject.FindGameObjectWithTag("stats_UI_text").GetComponent<Text>();
        roomTransferText_theObject = GameObject.FindGameObjectWithTag("room_transfer_text");
        roomTransferText = GameObject.FindGameObjectWithTag("room_transfer_text").GetComponent<Text>();
        tier_text = GameObject.FindGameObjectWithTag("tier_text").GetComponent<Text>();
        exp_text = GameObject.FindGameObjectWithTag("experience_text").GetComponent<Text>();
        attack_damage_text = GameObject.FindGameObjectWithTag("attack_damage_text").GetComponent<Text>(); 
        roomTransferText_theObject.SetActive(false); 
        dialogBox.SetActive(false);
        stats_UI.SetActive(false);
        HealPotionIcon.SetActive(false);

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

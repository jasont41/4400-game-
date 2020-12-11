using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_controller_battle : MonoBehaviour
{
    public string newLevel;
    public Vector3 spawnLocation;
    public GameObject enemyHealthBar; 
    // Start is called before the first frame update
    void Start()
    {
        //enemyHealthBar.SetActive(false); 
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void run_button_press()
    {
        PlayerMovement.Instance.transform.position = spawnLocation;
        PlayerMovement.Instance.prevent_movement = true;
        PlayerPrefs.SetString("PreviousScene", "Encounter"); 
        SceneManager.LoadScene(newLevel);
    }




}

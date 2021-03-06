﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.SceneManagement; 
 

public class encounter_trigger : MonoBehaviour
{
    public Vector3 spawnLocation; 
    public int random_num;
    public int encounter_seed = 1;
    int minBound = 1;
    int maxBound = 4;
    public string encounter_scene;
    //private PlayerMovement player;
    public EncounterManager encounterCont; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        random_num = UnityEngine.Random.Range(minBound, maxBound);
        if (random_num == encounter_seed && other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("EnemyLevel", encounterCont.level); 
            PlayerMovement.Instance.enemyPrefab = encounterCont.chosePrefab();
            //PlayerMovement.Instance.setPOS(transform.position);
            PlayerMovement.Instance.player_pos_before_encounter = PlayerMovement.Instance.transform.position; 
            PlayerMovement.Instance.transform.position = spawnLocation;
            SceneManager.LoadScene(encounter_scene);
            PlayerMovement.Instance.getPOS();
            PlayerMovement.Instance.prevent_movement = false; // prevents player from walking around the scene 
            PlayerMovement.Instance.setMovingFalse(); //stops player from moving in place
            Debug.Log("it works "); 
            Debug.Log(PlayerMovement.Instance.getPOS()); 
        }         
    }



}

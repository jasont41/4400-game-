﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string newLevel;
    public Vector3 spawnLocation; //tag of sprite in the scene I am moving too 
    public string previousLevel; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Player")){
            Debug.Log("Got here");
            PlayerPrefs.SetString("PreviousScene", previousLevel); 
            SceneManager.LoadScene(newLevel);  // lead new scene 
       }
    }

}

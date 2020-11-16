using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.SceneManagement;
public class RandomEncounterTest : MonoBehaviour
{
    public Vector3 spawnLocation;
    public int random_num;
    public int encounter_seed = 1;
    int minBound = 1;
    int maxBound = 4;
    public string encounter_scene;
    public GameObject EnemyPrefab; 
    
    public encounterControllerMainRoom encounterCont;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        random_num = UnityEngine.Random.Range(minBound, maxBound);
        if (random_num == encounter_seed && other.CompareTag("Player"))
        {
            PlayerMovement.Instance.enemyPrefab = encounterCont.selectedPrefab;
            PlayerMovement.Instance.enemyLevel = encounterCont.enemyLevel; 
            PlayerMovement.Instance.player_pos_before_encounter = PlayerMovement.Instance.transform.position;
            PlayerMovement.Instance.transform.position = spawnLocation;
            PlayerMovement.Instance.prevent_movement = false;
            PlayerMovement.Instance.setMovingFalse(); //stops player from moving in place
            SceneManager.LoadScene(encounter_scene); // prevents player from walking around the scene    
        }
    }
}

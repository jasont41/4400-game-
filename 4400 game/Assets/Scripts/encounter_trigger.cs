using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; 
 

public class encounter_trigger : MonoBehaviour
{
    public Vector3 spawnLocation; 
    public int random_num;
    public int encounter_seed = 1;
    int minBound = 1;
    int maxBound = 3;
    public string encounter_scene;
    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {//
        random_num = UnityEngine.Random.Range(minBound, maxBound);
        if (random_num == encounter_seed && other.CompareTag("Player"))
        {

           
            PlayerMovement.Instance.setPOS(transform.position);
            PlayerMovement.Instance.transform.position = spawnLocation;
            SceneManager.LoadScene(encounter_scene);
            PlayerMovement.Instance.getPOS();

            Debug.Log("it works "); 
            Debug.Log(PlayerMovement.Instance.getPOS()); 
        }         
    }



}

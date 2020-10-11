using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; 
 

public class encounter_trigger : MonoBehaviour
{

    public int random_num;
    public int encounter_seed = 5;
    public string encounter_scene; 
    // Start is called before the first frame update
    void Start()
    {
        random_num = UnityEngine.Random.Range(1, 10);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (random_num == encounter_seed)
        {
            SceneManager.LoadScene(encounter_scene); 
        }         
    }



}

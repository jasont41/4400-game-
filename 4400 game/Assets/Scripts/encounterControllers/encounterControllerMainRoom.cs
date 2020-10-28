using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class encounterControllerMainRoom : MonoBehaviour
{
    public int enemyLevel;


    public GameObject selectedPrefab; 
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2; 
   


    public GameObject chosePrefab()
    {
        int random_val = UnityEngine.Random.Range(1, 3);
        if (random_val == 1)
        {
            return enemyPrefab1;
        }
        else
        {
            return enemyPrefab2;
        }
    }
}

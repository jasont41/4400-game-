using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject selectedEnemy;
    public int level; 
    public GameObject chosePrefab()
    {
        int random_val = UnityEngine.Random.Range(0, enemies.Length);
        return enemies[0]; 
    }

}
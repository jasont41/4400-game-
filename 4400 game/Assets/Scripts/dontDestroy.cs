using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    public static dontDestroy Instance { get; private set; }
    private Vector3 player_pos_before_encounter;
    public void setPOS(Vector3 temp)
    {
        player_pos_before_encounter = temp; 
    }
    public Vector3 getPOS()
    {
        return player_pos_before_encounter; 
    }
    private void Awake()
    {
        if (Instance == null) { 
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }
}
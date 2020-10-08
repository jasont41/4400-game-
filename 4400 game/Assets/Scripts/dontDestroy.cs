using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    public static dontDestroy Instance { get; private set; }

    
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
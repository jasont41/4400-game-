using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dontDestroyOnLoad_health_bar : MonoBehaviour
{

    public static dontDestroyOnLoad_health_bar healthBar;
        
    public Slider slider;
    // Start is called before the first frame update
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    private void Awake()
    {
        if (healthBar == null)
        {
            DontDestroyOnLoad(gameObject);
            healthBar = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}


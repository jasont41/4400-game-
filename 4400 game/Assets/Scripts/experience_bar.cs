using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class experience_bar : MonoBehaviour
{
    public static experience_bar exp_bar;
    public Slider slider;

    public void setMaxExperience(int val)
    {
        slider.maxValue = val; 
    }
    public void setExperience(int exp_val)
    {
        slider.value = exp_val;
    }
    public void addExperience(int more_exp)
    {
        slider.value += more_exp;
    }

    public void Update()
    {
        slider.value = PlayerMovement.Instance.player_experience; 
    }

    private void Awake()
    {
        if(exp_bar == null)
        {
            DontDestroyOnLoad(gameObject);
            exp_bar = this; 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats_controller : MonoBehaviour
{
    public GameObject stats_ui;
    public Text healthText; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (stats_ui.activeInHierarchy)
            {
                stats_ui.SetActive(false);
            }
            else
            {
                stats_ui.SetActive(true);
                printStats();
            }
        }
    }

    private void printStats()
    {
        healthText.text = "Current Health " + PlayerMovement.Instance.current_health.ToString() + " / " + "Max Health " 
            + PlayerMovement.Instance.max_health.ToString(); 
    }
}

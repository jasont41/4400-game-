using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats_controller : MonoBehaviour
{
   //GameObject stats_ui;
   //Text healthText;



    // Start is called before the first frame update
    void Start()
    {
       // stats_ui = GameObject.FindGameObjectWithTag("stats_UI");
        //healthText = GameObject.FindGameObjectWithTag("stats_UI_text").GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvas_dont_destroy.Instance.stats_UI.activeInHierarchy)
            {
                canvas_dont_destroy.Instance.stats_UI.SetActive(false);
            }
            else
            {
                canvas_dont_destroy.Instance.stats_UI.SetActive(true);
                printStats();
            }
        }
    }

    private void printStats()
    {
        canvas_dont_destroy.Instance.stats_UI_text.text = "Current Health " + PlayerMovement.Instance.current_health.ToString() + " / " + "Max Health " 
            + PlayerMovement.Instance.max_health.ToString(); 
    }
}

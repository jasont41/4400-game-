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

        canvas_dont_destroy.Instance.tier_text.text = "Tier = " + PlayerMovement.Instance.player_tier.ToString() ;

        canvas_dont_destroy.Instance.exp_text.text = "PF: " + "Experience = " + PlayerMovement.Instance.player_experience.ToString() +
            "\nExperience left until next tier: " + (PlayerMovement.Instance.experienceForNextTier - PlayerMovement.Instance.player_experience).ToString();
        canvas_dont_destroy.Instance.MoneyText.text = "$" + PlayerMovement.Instance.returnMoneyAmt(); 
        canvas_dont_destroy.Instance.attack_damage_text.text = "Attack Damage = " + PlayerMovement.Instance.attack_damage.ToString(); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_up_script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int throw_away = PlayerMovement.Instance.experienceForNextTier - PlayerMovement.Instance.player_experience; 
            PlayerMovement.Instance.add_experience(throw_away);
            PlayerMovement.Instance.checkLevelUP(); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healTestScript : MonoBehaviour
{
    int heal = 10;
   // public PlayerMovement player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.Instance.addHealth(heal);
            //player.takeDamage(damage);
        }
    }
}

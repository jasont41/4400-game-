using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageTestScript : MonoBehaviour
{
    int damage = 10;
    public PlayerMovement player; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.Instance.takeDamage(damage); 
            //player.takeDamage(damage);
        }
    }
}

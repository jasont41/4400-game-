using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    public string unitName;
    public int damage;
    public int level; 
    public int maxHP;
    public int currentHP;
    public int exp_given = 5;
    public int money_given = 2; 
    public void Start()
    {
        EncounterManager encMan = gameObject.GetComponentInParent<EncounterManager>();
        if(encMan != null)
            level = encMan.level;
        else
        {
            level = 1; 
        }
        for (int i = 1; i < level; i++)
        {
            damage = (int)(damage + (damage * 0.2f)); // adding 20% of value for each level enemy is above base stats
            maxHP = (int)(maxHP + (maxHP * 0.2f));
            money_given = (int)(money_given + (money_given * 0.2f)); 
        }
    }
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}

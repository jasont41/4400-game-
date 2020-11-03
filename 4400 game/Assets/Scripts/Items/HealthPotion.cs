using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Items
{
    public int heal_val = 20; 
    // Start is called before the first frame update
    public override void useItem()
    {
        PlayerMovement.Instance.addHealth(heal_val); 
    }

}

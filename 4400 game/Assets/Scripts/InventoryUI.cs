using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] inventoryContents;
    public int inventoryIndex; 

    void checkInentory()
    {
        for(int i = 0; i < inventoryContents.Length; i++)
        {
            if(inventoryContents[i].name == null) //for finding the next empty inventory slot
            {
                inventoryIndex = i; 
            }
        }
    }
    public void addItem(GameObject newItem)
    {
        checkInentory(); 
        for(int i = 0; i < inventoryContents.Length; i++)
        {
            if(inventoryContents[i].name == newItem.name)
            {
                //inventoryContents[i].addItem(newItem.name); 
            }
        }
    }

}

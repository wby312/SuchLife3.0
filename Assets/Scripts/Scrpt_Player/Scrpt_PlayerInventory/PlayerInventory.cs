using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{

    public List<InventorySlot> inventory; 

    public GameObject inventoryPanel; 

    public List<GameObject> hotbar = new List<GameObject>(); 

  // public GameObject[] hotbar;

    int inventorySize = 40; 

    void Start(){

        inventory = new List<InventorySlot>();
        //Get panels as hotbar.
        
        //Get all children of inventoryPanel (So every hotbar button) and add them to Hotbar.
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
            {
                hotbar.Add(inventoryPanel.transform.GetChild(i).gameObject);
            }    

        for(int i = 0; i < inventorySize; i++)
        {
            InventorySlot slot = new InventorySlot(); 
            slot.isEmpty = true;
            inventory.Add(slot); 
        }


    }
 


    public void AddItem(Item item){
        Debug.Log("Adding Item...");

        //If isInHotbar is 0 - 9, it's in the hotbar!
        int isInHotbar = 0; 
        foreach (InventorySlot slot in inventory){

            if(slot.item == item){
                        Debug.Log("Already exists, increasing quantity!");

                //Item already exists in inventory, therefore, we increment its amount.
                slot.quantity++; 
                return; 
            }
            isInHotbar++; 
        }

        //If we go through entire loop and havent found a match, we simply add it to next free slot.
         foreach (InventorySlot slot in inventory){
            Debug.Log("Adding to next free slot!");

            if(slot.isEmpty == true){
                slot.isEmpty = false; 
                slot.item = item; 
                slot.quantity++; 
                return; 
            }
        }

        

    }

   public void RemoveItem(Item item){
            Debug.Log("Removing Item...");

        foreach (InventorySlot slot in inventory){
            if(slot.item == item){
                Debug.Log("Removing Item");

                slot.quantity--; 

                if(slot.quantity == 0){
                     Debug.Log("0 quantity, emptying slot.");

                    slot.isEmpty = true; 
                    slot.item = null;
                }
                return; 
            }
        }

    }

   public void PrintAllItemsToDebug(){
        foreach (InventorySlot slot in inventory){
            if(slot.isEmpty == false){
              Debug.Log(slot.item.itemName + " : Quantity " + slot.quantity );

            }
        }
    }

    public List<InventorySlot> getInventory(){
        return inventory; 
    }




}

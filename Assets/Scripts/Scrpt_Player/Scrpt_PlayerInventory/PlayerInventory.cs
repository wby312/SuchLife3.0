using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    //attached to Player. UI be able to get inventory.

    InventorySlot[] inventory; 
    int inventorySize = 40; 


    void AddItem(Item item){
        foreach (InventorySlot slot in inventory){
            if(slot.item == item){
                //Item already exists in inventory, therefore, we increment its amount.
                slot.quantity++; 
                return; 
            }
        }

        //If we go through entire loop and havent found a match, we simply add it to next free slot.
           foreach (InventorySlot slot in inventory){
            if(slot.isEmpty == true){
                slot.isEmpty = false; 
                slot.item = item; 
            }
        }

    }

    void RemoveItem(Item item){
        foreach (InventorySlot slot in inventory){
            if(slot.item == item){
                slot.quantity--; 

                if(slot.quantity == 0){
                    slot.isEmpty = true; 
                    slot.item = null;
                }
                return; 
            }
        }

    }

    void PrintAllItemsToDebug(){
        foreach (InventorySlot slot in inventory){
            if(slot.isEmpty == false){
               Debug.Log(slot.item + " : Quantity " + slot.quantity );
               
            }
        }
    }

    InventorySlot[] getInventory(){
        return inventory; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

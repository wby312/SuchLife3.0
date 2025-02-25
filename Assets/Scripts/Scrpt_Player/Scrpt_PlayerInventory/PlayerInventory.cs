using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    //attached to Player. UI be able to get inventory.

    InventorySlot[] inventory; 
    int inventorySize = 40; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void AddItem(Item item){
        //for(int i = 0; i < inventory.Count; i++){
        //
        // }

    }

    void RemoveItem(){

    }

    InventorySlot[] getInventory(){
        return inventory; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

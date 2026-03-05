using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnInventoryChanged;

    [HideInInspector]
    public UnityEvent<InventoryItem> OnInventorySpawned;

    [HideInInspector]
    public UnityEvent OnInventoryFull;

    public Dictionary<InventoryItem, int> inventory = new Dictionary<InventoryItem, int>();

    public InventoryItem activeItem = InventoryItem.Coin;

    public void Awake()
    {
        if (OnInventoryChanged == null)
        {
            OnInventoryChanged = new UnityEvent();
        }
        if (OnInventorySpawned == null)
        {
            OnInventorySpawned = new UnityEvent<InventoryItem>();
        }
        if (OnInventoryFull == null)
        {
            OnInventoryFull = new UnityEvent();
        }
        //initialize all inventory item keys
        inventory[InventoryItem.Chest] = 0;
        inventory[InventoryItem.Lantern] = 0;
        inventory[InventoryItem.Coin] = 0;
    }

    public void PickUpInventory(Inventory inventoryComponent)
    {
        // set a carry limit of 2
        if (inventory[inventoryComponent.item] < 2)
    {
            // the key is guaranteed to exist here
            inventory[inventoryComponent.item] += 1;
            OnInventoryChanged.Invoke();

            Destroy(inventoryComponent.gameObject);
        }
        else
        {
            // over the carry limit!
            OnInventoryFull.Invoke();
        }
    }

    public void DropInventory()
    {
        if (inventory[activeItem] > 0)
        {
            inventory[activeItem] -= 1;
            OnInventoryChanged.Invoke();

            OnInventorySpawned.Invoke(activeItem);
        }
    }


}

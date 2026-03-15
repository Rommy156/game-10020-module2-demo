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

    [HideInInspector]
    public UnityEvent<bool> OnInteract;


    public Dictionary<InventoryItem, int> inventory = new Dictionary<InventoryItem, int>();

    public InventoryItem activeItem = InventoryItem.Coin;

    public static class GameEvents
    {
        public static System.Action<int> OnCoinCollected;
        public static System.Action<int> OnCoinsChanged;
        public static System.Action<string> OnItemPurchased;
    }
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
        if (OnInteract == null)
        {
            OnInteract = new UnityEvent<bool>();
        }

        //initialize all inventory item keys
        inventory[InventoryItem.Chest] = 0;
        inventory[InventoryItem.Lantern] = 0;
        inventory[InventoryItem.Coin] = 0;
        inventory[InventoryItem.Key] = 0;
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


    public bool SpendCoins(int amount)
    {
        if (inventory[InventoryItem.Coin] >= amount)
        {
            inventory[InventoryItem.Coin] -= amount;

            OnInventoryChanged.Invoke();

            Debug.Log("Coins remaining: " + inventory[InventoryItem.Coin]);

            return true;
        }

        Debug.Log("Not enough coins");

        return false;
    }
}

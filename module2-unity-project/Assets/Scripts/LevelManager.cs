using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    [Header("Managers")]
    public UIManager uiManager;
    public InventoryManager inventoryManager;

    [Header("Character Controller")]
    public Character character;

    [Header("Gameplay Objects")]
    public GameObject barriers1;
    public Toggle toggle1;
    public WallEye wallEye;
    public Door door;
    public GameObject inventoryItems;
    public Chest chest;

    [Header("Prefabs")]
    public Inventory coinPrefab;
    public Inventory lanternPrefab;
    public Inventory chestPrefab;
    public Inventory keyPrefab;

    public ShopTerminal shop;


    // the level manager is responsible for connecting the core game system events
    // notice that these events have arguments - it's not possible to pass arguments to
    // events in Unity when using the Editor (what we did in Module 1)
    // arguments make the events more flexible

    private void Start()
    {
        // connect inventory and UI events
        inventoryManager.OnInventoryChanged.AddListener(uiManager.UpdateInventoryUI);
        inventoryManager.OnInventoryChanged.AddListener(LockDoorInventory);
        inventoryManager.OnInventorySpawned.AddListener(SpawnInventory);
        inventoryManager.OnInventoryFull.AddListener(uiManager.ShowInventoryFull);

        shop.OnItemPurchaseRequested.AddListener(ProcessShopPurchase);


        void ProcessShopPurchase(InventoryItem item)
        {
            int cost = 1;

            ShopTerminal shop = FindObjectOfType<ShopTerminal>();

            if (inventoryManager.SpendCoins(cost))
            {
                Debug.Log("Item Purchased: " + item);

                inventoryManager.inventory[item] += 1;

                inventoryManager.OnInventoryChanged.Invoke();

                shop.PurchaseResult(true);
            }
            else
            {
                shop.PurchaseResult(false);
            }
        }
        //add listener for shop terminal purchase event
        shop.OnPurchaseSuccess.AddListener(OnItemPurchased);

        //listener for chest
        chest.OnChestOpened.AddListener(OpenDoorFromChest);

        foreach (Transform child in inventoryItems.transform)
        {
            Inventory inventory = child.GetComponent<Inventory>();
            inventory.OnItemCollected.AddListener(inventoryManager.PickUpInventory);
        }

        // connect gameplay system events
        foreach (Transform child in barriers1.transform)
        {
            Barrier barrier = child.GetComponent<Barrier>();
            toggle1.OnToggle.AddListener(barrier.Move);
        }

        toggle1.OnToggle.AddListener(wallEye.OpenClose);
        wallEye.OnEyeStateChanged.AddListener(LockDoorWallEye);

        character.OnInventoryShown.AddListener(uiManager.ShowInventory);
        character.OnItemDropped.AddListener(inventoryManager.DropInventory);
    }
    // we "buffer" the event with a function that has the correct arguments

    void LockDoorWallEye(WallEyeState eyeState)
    {
        if (eyeState == WallEyeState.Defeated)
        {
            door.SetLock(false);
        }
        else
        {
            door.SetLock(true);
        }
    }
    void OpenDoorFromChest()
    {

        door.SetLock(false);
    }

    void LockDoorInventory()
    {
        if (inventoryManager.inventory[InventoryItem.Chest] > 2)
        {
            door.SetLock(false);
        }
        else
        {
            door.SetLock(true);
        }
    }
    void OnItemPurchased()
    {
        Debug.Log("Purchase successful");

        uiManager.UpdateInventoryUI();
    }
    void SpawnInventory(InventoryItem item)
    {
        switch (item)
        {
            case InventoryItem.Chest:
                PlaceInventory(chestPrefab);
                break;
            case InventoryItem.Lantern:
                PlaceInventory(lanternPrefab);
                break;
            case InventoryItem.Coin:
                PlaceInventory(coinPrefab);
                break;
            case InventoryItem.Key:
                PlaceInventory(keyPrefab);
                break;
        }
    }

    void PlaceInventory(Inventory inventoryPrefab)
    {
        Inventory inventory = Instantiate(inventoryPrefab);
        inventory.OnItemCollected.AddListener(inventoryManager.PickUpInventory);

        // drop the new inventory item at the player position and a little
        // bit in front of it the player
        inventory.transform.position = character.transform.position + character.transform.forward * 1.0f;
    }
    
   
}

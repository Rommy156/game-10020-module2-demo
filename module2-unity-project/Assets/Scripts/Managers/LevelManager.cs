using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // managers
    public InventoryManager inventoryManager;
    public UIManager uiManager;
    public GameObject inventory;

    public Inventory lantern;

    // game system objects
    public GameObject barriers1;
    public Toggle toggle1;
    public WallEye wallEye;
    public Door door;

    // the level manager is responsible for connecting the core game system events
    // notice that these events have arguments - it's not possible to pass arguments to
    // events in Unity when using the Editor (what we did in Module 1)
    // arguments make the events more flexible

    private void Start()
    {
        // inventory events
        inventoryManager.OnInventoryChanged.AddListener(uiManager.UpdateInventoryUI);
        foreach (Transform child in inventory.transform)
        {
            Inventory inventory = child.GetComponent<Inventory>();
            inventory.OnItemCollected.AddListener(inventoryManager.PickUpInventory);
        }
        
        // game system events
        foreach (Transform child in barriers1.transform)
        {
            Barrier barrier = child.GetComponent<Barrier>();
            toggle1.OnToggle.AddListener(barrier.Move);
        }

        toggle1.OnToggle.AddListener(wallEye.OpenClose);

        wallEye.OnEyeStateChanged.AddListener(LockDoorWallEye);

        // this unlocks the door for this SPECIFIC lantern
        //lantern.OnItemCollected.AddListener(LockDoorItemPickup);
        inventoryManager.OnInventoryChanged.AddListener(LockDoorAnyItemPickup);
    }

    // "buffer" event function
    void LockDoorWallEye(WallEyeState eyeState)
    {
        LockDoor(eyeState, false);
    }

    void LockDoorItemPickup(InventoryItem item)
    {
        if (item == InventoryItem.Lantern)
        {
            LockDoor(WallEyeState.Closed, true);
        }
    }

    void LockDoorAnyItemPickup()
    {
        if (inventoryManager.inventory[InventoryItem.Lantern] > 0)
        {
            LockDoor(WallEyeState.Closed, true);
        }
    }
    
    void LockDoor(WallEyeState eyeState, bool keyCollected)
    {
        if (eyeState == WallEyeState.Defeated || keyCollected)
        {
            door.SetLock(false);
        }
        else
        {
            door.SetLock(true);
        }
    }
}

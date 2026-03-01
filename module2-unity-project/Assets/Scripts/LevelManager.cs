using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject barriers1;
    public Toggle toggle1;
    public WallEye wallEye;
    public Door door;

    public Pumpkin pumpkinPrefab;

    public Pumpkin pumpkin1;
    public Pumpkin pumpkin2;

    public UIManager uiManager;
    public InventoryManager inventoryManager;

    // the level manager is responsible for connecting the core game system events
    // notice that these events have arguments - it's not possible to pass arguments to
    // events in Unity when using the Editor (what we did in Module 1)
    // arguments make the events more flexible

    private void Start()
    {
        foreach (Transform child in barriers1.transform)
        {
            Barrier barrier = child.GetComponent<Barrier>();
            toggle1.OnToggle.AddListener(barrier.Move);
        }

        toggle1.OnToggle.AddListener(wallEye.OpenClose);

        wallEye.OnEyeStateChanged.AddListener(LockDoor);

        // inventory events
        pumpkin1.OnInventoryCollect.AddListener(inventoryManager.PickUpInventory);
        pumpkin2.OnInventoryCollect.AddListener(inventoryManager.PickUpInventory);
        
        inventoryManager.OnInventoryChanged.AddListener(uiManager.UpdateInventoryUI);
    }

    public void DropPumpkin()
    {
        if (inventoryManager.inventory[InventoryItem.Pumpkin] > 0)
        {
            inventoryManager.DropInventory(InventoryItem.Pumpkin);

            Pumpkin newPumpkin = Instantiate(pumpkinPrefab);
            newPumpkin.transform.position = new Vector3(-3, 0, -1);
            newPumpkin.OnInventoryCollect.AddListener(inventoryManager.PickUpInventory);
        }
    }

    void LockDoor(WallEyeState eyeState)
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
    
}

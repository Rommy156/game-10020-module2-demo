using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDoorUnlock : MonoBehaviour
{
    public Door door;

    public void UnlockDoor()
    {
        door.SetLock(false);
        Debug.Log("Door unlocked from shop purchase");
    }
}
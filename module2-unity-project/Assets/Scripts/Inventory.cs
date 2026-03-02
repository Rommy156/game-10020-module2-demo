using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour, IHittable
{
    public InventoryItem item;

    [HideInInspector]
    public UnityEvent<InventoryItem> OnItemCollected;

    public void Awake()
    {
        if (OnItemCollected == null) OnItemCollected = new UnityEvent<InventoryItem>();
    }

    public void Hit(GameObject otherGameObject)
    {
        OnItemCollected.Invoke(item);

        // this potentially has issues - what if the player is not able to pick
        // up an item?
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pumpkin : MonoBehaviour, IHittable
{
    [HideInInspector]
    public UnityEvent<InventoryItem> OnInventoryCollect;

    public void Awake()
    {
        if (OnInventoryCollect == null)
        {
            OnInventoryCollect = new UnityEvent<InventoryItem>();
        }
    }

    public void Hit(GameObject gameObject)
    {
        OnInventoryCollect.Invoke(InventoryItem.Pumpkin);
        Destroy(this.gameObject);
    }
}

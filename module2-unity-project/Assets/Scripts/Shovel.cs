using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    Collider weaponCollider;
    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
    }

    // we are using an int here because it plays nicely with the Animation controller
    public void EnableHitbox(int value)
    {
        weaponCollider.enabled = value == 1 ? true : false;
    }

    // here is where we see the power of Interfaces
    // all we need to do is check if the collided object has an
    // IHittable interface. we don't care what it is, so long as it has it
    // if it has it, simply call the Hit() method
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.name);

        IHittable hittable = other.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittable.Hit(gameObject);
            return;
        }

        IInteractable interactable = other.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact(gameObject);
        }
    }
}

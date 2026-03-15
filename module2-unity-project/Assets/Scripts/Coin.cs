using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor)
    {
        Character character = interactor.GetComponent<Character>();

        if (character != null)
        {
            InventoryManager inventory = character.inventory;

            inventory.inventory[InventoryItem.Coin] += 1;

            inventory.OnInventoryChanged.Invoke();

            Debug.Log("Coin collected. Total: " + inventory.inventory[InventoryItem.Coin]);
        }

        Destroy(gameObject);
    }
}
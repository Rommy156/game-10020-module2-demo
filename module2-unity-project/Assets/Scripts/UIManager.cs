using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public TextMeshProUGUI pumpkinsText;
    public TextMeshProUGUI lanternsText;
    public void UpdateInventoryUI()
    {
        // get the inventory quantity directly from the inventory manager
        int pumpkinsInventory = inventoryManager.inventory[InventoryItem.Pumpkin];
        pumpkinsText.text = $"Pumpkins: {pumpkinsInventory}";

        int lanternsInventory = inventoryManager.inventory[InventoryItem.Lantern];
        lanternsText.text = $"Lanterns: {lanternsInventory}";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public TextMeshProUGUI pumpkinsText;
    public TextMeshProUGUI lanternsText;
    public TextMeshProUGUI coffinsText;

    public void UpdateInventoryUI()
    {
        pumpkinsText.text = $"Pumpkins: {inventoryManager.inventory[InventoryItem.Pumpkin]}";
        lanternsText.text = $"Lanterns: {inventoryManager.inventory[InventoryItem.Lantern]}";
        coffinsText.text = $"Coffins: {inventoryManager.inventory[InventoryItem.Coffin]}";
    }

}

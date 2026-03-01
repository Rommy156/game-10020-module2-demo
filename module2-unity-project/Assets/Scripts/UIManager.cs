using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public TextMeshProUGUI pumpkinText;
    
    void UpdatePumpkinUI()
    {
        pumpkinText.text = $"Pumpkins: {inventoryManager.inventory[InventoryItem.Pumpkin]}";
    }

    public void UpdateInventoryUI()
    {
        UpdatePumpkinUI();
        // UpdateLanternUI()
        // etc...
    }


}

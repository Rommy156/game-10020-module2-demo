using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI lanternsText;
    public TextMeshProUGUI chestText;

    public GameObject inventoryPanel;

    public TextMeshProUGUI activeInventoryText;
    public InventoryFull inventoryFull;

    public void Awake()
    {
        inventoryPanel.SetActive(false);
    }
    public void UpdateInventoryUI()
    {
        // get the inventory quantity directly from the inventory manager
        int coinInventory = inventoryManager.inventory[InventoryItem.Coin];
        coinText.text = $"Coins: {coinInventory}";

        int lanternsInventory = inventoryManager.inventory[InventoryItem.Lantern];
        lanternsText.text = $"Lanterns: {lanternsInventory}";

        int chestInventory = inventoryManager.inventory[InventoryItem.Chest];
        chestText.text = $"Chests: {chestInventory}";
    }

    public void ShowInventory(bool show)
    {
        inventoryPanel.SetActive(show);
    }
    public void SetActiveInventory(InventoryItem item)
    {
        inventoryManager.activeItem = item;
        activeInventoryText.text = $"Active Inventory: {item}";
    }
    public void SetCoinActive()
    {
        SetActiveInventory(InventoryItem.Coin);
    }
    public void SetLanternsActive()
    {
        SetActiveInventory(InventoryItem.Lantern);
    }
    public void SetChestActive()
    {
        SetActiveInventory(InventoryItem.Chest);
    }

    public void ShowInventoryFull()
    {
        inventoryFull.ShowInventoryFull();
    }
}

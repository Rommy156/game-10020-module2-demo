using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using static InventoryManager;

public class UIManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI lanternsText;
    public TextMeshProUGUI chestText;

    public GameObject inventoryPanel;
    public GameObject purchaseFailedPanel;
    public PurchaseFailed purchaseFailed;

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
    public void ShowPurchasedFailed(bool show)
    {
        // show a message that the purchase failed
        Debug.Log("Purchase Failed: Not enough coins");

        purchaseFailedPanel.SetActive(show);
        purchaseFailed.ShowPurchaseFailedTextPanel();

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

    private void OnEnable()
    {
        GameEvents.OnCoinsChanged += UpdateUI;
    }

    private void OnDisable()
    {
        GameEvents.OnCoinsChanged -= UpdateUI;
    }

    void UpdateUI(int coins)
    {
        coinText.text = "Coins: " + coins;
    }
}


using UnityEngine;
using UnityEngine.Events;

public class ShopTerminal : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject shopMenu;

    public Character player;

    public Transform playerLeftHand;
    public GameObject keyPrefab;
    public GameObject lanternPrefab;

    bool playerInTrigger = false;

    public UnityEvent <InventoryItem> OnItemPurchaseRequested;

    public UnityEvent OnPurchaseSuccess;
    public UnityEvent OnPurchaseFailed;

    void Awake()
    {
        if (OnItemPurchaseRequested == null)
            OnItemPurchaseRequested = new UnityEvent<InventoryItem>();
    }

    void Start()
    {
        shopPanel.SetActive(false);
        shopMenu.SetActive(false);
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            shopMenu.SetActive(!shopMenu.activeSelf);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            playerInTrigger = true;
            shopPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            playerInTrigger = false;
            shopPanel.SetActive(false);
            shopMenu.SetActive(false);
        }
    }

    public void BuyKey()
    {
        OnItemPurchaseRequested.Invoke(InventoryItem.Key);
    }
   
    public void SpawnKey()
    {
      { GameObject key = Instantiate(keyPrefab, playerLeftHand);

        key.transform.localPosition = keyPrefab.transform.localPosition;
        key.transform.localRotation = keyPrefab.transform.localRotation;
        key.transform.localScale = keyPrefab.transform.localScale;

            key.tag = "Key";
        }
    }
    public void BuyLantern()
    {
        OnItemPurchaseRequested.Invoke(InventoryItem.Lantern);
    }

    public void SpawnLantern()
    {
        GameObject lantern = Instantiate(lanternPrefab, playerLeftHand);
        lantern.transform.localPosition = lanternPrefab.transform.localPosition;
        lantern.transform.localRotation = lanternPrefab.transform.localRotation;
        lantern.transform.localScale = lanternPrefab.transform.localScale;
        lantern.tag = "Lantern";
    }

    public void PurchaseResult(bool success)
    {
        if (success)
        {
            Debug.Log("Purchase successful");
            OnPurchaseSuccess?.Invoke();
        }
        else
        {
            OnPurchaseFailed?.Invoke();
            Debug.Log("Purchase failed");
        }

        shopMenu.SetActive(false);
        shopPanel.SetActive(false);

        if (player != null)
            player.SetShopState(false);
    }
}
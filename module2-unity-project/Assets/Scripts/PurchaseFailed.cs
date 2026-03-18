using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseFailed : MonoBehaviour
{
    public GameObject purchaseFailedPanel;
    Animator animator;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void ShowPurchaseFailedTextPanel()
    {

        gameObject.SetActive(true);
        animator.SetTrigger("SetShake");

    }
    public void HideInventoryFull()
    {
        gameObject.SetActive(false);
        purchaseFailedPanel.SetActive(false);
    }
}

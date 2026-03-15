using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using static InventoryManager;

public class Door : MonoBehaviour
{
    public Sprite doorLocked;
    public Sprite doorOpen;

    public String sceneName;

    bool lockedState = true;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void UpdateState()
    {
        spriteRenderer.sprite = lockedState ? doorLocked : doorOpen;
    }

    public void SetLock(bool lockState)
    {
        lockedState = lockState;
        UpdateState();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!lockedState && other.gameObject.CompareTag("character"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    private void OnEnable()
    {
        GameEvents.OnItemPurchased += UnlockDoor;
    }

    private void OnDisable()
    {
        GameEvents.OnItemPurchased -= UnlockDoor;
    }

    void UnlockDoor(string item)
    {
        if (item == "Key")
        {
            gameObject.SetActive(false);
        }
    }
}

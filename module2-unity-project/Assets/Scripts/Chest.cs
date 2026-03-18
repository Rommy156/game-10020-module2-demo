using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    public UnityEvent OnChestOpened;
    public GameObject door;

    //if Key tag is in trigger area then OnChestOpened is called  
    // if OnChestOpened is callld, set the door to false  and destroy Key
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Debug.Log("Chest unlocked!");

            OnChestOpened?.Invoke();

            if (door != null)
                door.SetActive(false);

            Destroy(other.gameObject);
        }
    }
}
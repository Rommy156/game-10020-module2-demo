using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    public UnityEvent OnChestOpened;
    public GameObject door;

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
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public class Keycard : MonoBehaviour
{
    public UnityEvent OnKeycardCollected;

    private void OnTriggerEnter(Collider other)
    {
       if(other.TryGetComponent<FirstPersonController>(out var controller))
       {
            if (controller != null)
            {
                OnKeycardCollected?.Invoke();
                gameObject.SetActive(false);
            }
       }
    }
}

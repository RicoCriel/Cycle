using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _interactRange;

    public void TryInteract()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit,
            _interactRange, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }

        Debug.DrawRay(transform.position, transform.forward * _interactRange, Color.red, 1f);
    }
}

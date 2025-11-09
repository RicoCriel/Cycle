using UnityEngine;

public class Deactivator : MonoBehaviour
{
    private Collider _collider;
    private Rigidbody _rb;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Deactivate()
    {
        if (_rb != null)
        {
            _rb.isKinematic = true;
        }

        if( _collider != null )
        {
            _collider.enabled = false;
        }
    }
}

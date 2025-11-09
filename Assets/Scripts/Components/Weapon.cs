using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Camera _camera;
    private const float raycastDistance = 100f; 
    private RaycastHit _hit;
    [SerializeField] private int _damage;
    private LineRenderer _lineRenderer;

    public LayerMask HitLayer;

    private void Awake()
    {
        _camera = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Shoot()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out _hit, raycastDistance, HitLayer))
        {
            if(_hit.transform.TryGetComponent<Health>(out Health health))
            {
                health.DecreaseHealth(_damage);
            }
            //Debug.Log("Hit " + _hitInfo.collider.name + " at point " + _hitInfo.point);
        }

        //Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.yellow, 1f);
    }
}

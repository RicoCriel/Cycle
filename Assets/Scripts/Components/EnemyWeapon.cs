using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField]
    private Transform _barrel;
    [SerializeField]
    private ParticleSystem _impactParticleSystem;
    [SerializeField]
    private TrailRenderer _bulletTrail;
    [SerializeField]
    private float _bulletSpeed;
    private Coroutine _spawnTrail;

    public LayerMask HitMask;

    public void Shoot()
    {
        Vector3 direction = GetDirection();

        if (Physics.Raycast(_barrel.position, direction, out RaycastHit hit, float.MaxValue, HitMask))
        {
            TrailRenderer trail = Instantiate(_bulletTrail, _barrel.position, Quaternion.identity);
            if (_spawnTrail != null)
            {
                StopCoroutine(_spawnTrail);
            }
            _spawnTrail = StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
        }
        else
        {
            TrailRenderer trail = Instantiate(_bulletTrail, _barrel.position, Quaternion.identity);
            if (_spawnTrail != null)
            {
                StopCoroutine(_spawnTrail);
            }
            _spawnTrail = StartCoroutine(SpawnTrail(trail, _barrel.position + direction * 100f, Vector3.zero, false));
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = _barrel.transform.forward;
        return direction.normalized;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint, Vector3 hitNormal, bool hasMadeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, hitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            if(trail != null)
            {
                trail.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (remainingDistance / distance));
                remainingDistance -= _bulletSpeed * Time.deltaTime;
            }

            yield return null;
        }
        if (trail != null)
        {
            trail.transform.position = hitPoint;
        }

        if (hasMadeImpact)
        {
            Instantiate(_impactParticleSystem, hitPoint, Quaternion.LookRotation(hitNormal));

            var hitObject = Physics.OverlapSphere(hitPoint, 1f, HitMask);
            foreach (var collider in hitObject)
            {
                ApplyDamage(collider, hitPoint);
            }
        }
    }

    private void ApplyDamage(Collider collider, Vector3 hitPoint)
    {
        if (collider.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }
}

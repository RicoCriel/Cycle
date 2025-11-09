using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private int _damage;
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private ParticleSystem _impactParticleSystem;
    [SerializeField]
    private TrailRenderer _bulletTrail;
    [SerializeField]
    private float _shootDelay;
    [SerializeField]
    private float _bulletSpeed;

    private Animator _animator;
    private float _lastShootTime;

    private Coroutine _resetState;
    private Coroutine _spawnTrail;

    public LayerMask HitMask;

    public UnityEvent<Vector3> OnEnemyHit;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
    }

    public void Shoot()
    {
        if (Time.time < _lastShootTime + _shootDelay)
            return;

        _lastShootTime = Time.time;

        _animator.SetBool("IsShooting", true);

        if(_resetState  != null)
        {
            StopCoroutine(_resetState);
        }
        _resetState = StartCoroutine(ResetShootState());

        Vector3 direction = GetDirection();

        if (Physics.Raycast(_bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, HitMask))
        {
            TrailRenderer trail = Instantiate(_bulletTrail, _bulletSpawnPoint.position, Quaternion.identity);
            if(_spawnTrail != null)
            {
                StopCoroutine(_spawnTrail);
            }
            _spawnTrail = StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
        }
        else
        {
            TrailRenderer trail = Instantiate(_bulletTrail, _bulletSpawnPoint.position, Quaternion.identity);
            if (_spawnTrail != null)
            {
                StopCoroutine(_spawnTrail);
            }
            _spawnTrail = StartCoroutine(SpawnTrail(trail, _bulletSpawnPoint.position + direction * 100f, Vector3.zero, false));
        }
        _lastShootTime = 0;
    }

    private Vector3 GetDirection()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 direction = ray.direction;
        return direction.normalized;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint, Vector3 hitNormal, bool hasMadeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, hitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= _bulletSpeed * Time.deltaTime;

            yield return null;
        }
        if(trail != null)
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
                OnEnemyHit?.Invoke(hitPoint);
            }
        }
    }

    private IEnumerator ResetShootState()
    {
        yield return new WaitForSeconds(_shootDelay);
        _animator.SetBool("IsShooting", false);
    }

    private void ApplyDamage(Collider collider, Vector3 hitPoint)
    {
        if (collider.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }
}

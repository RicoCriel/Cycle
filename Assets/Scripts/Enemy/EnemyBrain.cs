using StarterAssets;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBrain : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;
    private Animator _animator;
    private StateMachine _stateMachine;
    private Health _health;
    private EnemyModel _model;
    private Transform _transform;

    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _visionDistance;
    [SerializeField] private float _visionAngle;
    [SerializeField] private float _attackRange;

    public EnemyPatrolState PatrolState;
    public EnemyChaseState ChaseState;
    public EnemyAttackState AttackState;
    public EnemyDeadState DeadState;

    public StateMachine StateMachine { get { return _stateMachine; } }
    public NavMeshAgent Agent { get { return _agent; } }
    public Transform Target { get { return _target; } }
    public Animator Animator { get { return _animator; } }
    public Health Health { get { return _health; } }
    public EnemyModel Model { get { return _model; } }
    public Transform[] PatrolPoints { get { return _patrolPoints; } }
    public Transform Transform { get { return _transform; } }

    public float VisionDistance { get { return _visionDistance; } }
    public float VisionAngle { get { return _visionAngle; } }
    public float AttackRange { get { return _attackRange; } }

    private List<Transform> _enemyPatrolPoints = new List<Transform>();
    public List<Transform> EnemyPatrolPoints { get { return _enemyPatrolPoints; } }
    public UnityEvent OnDeath;

    private void Awake()
    {
        _model = new EnemyModel();
        _transform = this.transform;
        _agent = GetComponent<NavMeshAgent>();
        _target = FindFirstObjectByType<FirstPersonController>().transform;
        _enemyPatrolPoints.AddRange(PatrolPoints);
        _enemyPatrolPoints = PatrolPoints.OrderBy(waypoint => waypoint.name).ToList();

        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();

        _stateMachine = new StateMachine();
        PatrolState = new EnemyPatrolState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        DeadState = new EnemyDeadState(this);
    }

    private void Start()
    {
        _stateMachine.ChangeState(PatrolState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}

//#if UNITY_EDITOR
//    private void OnDrawGizmosSelected()
//    {
//        if (!Application.isPlaying)
//        {
//            _transform = transform;
//        }

//        // Vision range 
//        Gizmos.color = Color.green;
//        Handles.color = new Color(0f, 1f, 0f, 0.15f);
//        Handles.DrawSolidArc(
//            _transform.position,
//            Vector3.up,
//            Quaternion.Euler(0, -_visionAngle, 0) * _transform.forward,
//            _visionAngle * 2f,
//            _visionDistance
//        );
//        Gizmos.DrawWireSphere(_transform.position, _visionDistance);

//        // Attack range 
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(_transform.position, _attackRange);

//        // Draw line toward player if target exists
//        if (_target != null)
//        {
//            Gizmos.color = Color.yellow;
//            Gizmos.DrawLine(_transform.position, _target.position);
//        }
//    }
//#endif
//}


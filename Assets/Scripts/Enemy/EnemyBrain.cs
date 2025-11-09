using StarterAssets;
using System.Collections.Generic;
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

    [SerializeField] private float _patrolRadius;
    [SerializeField] private float _visionDistance;
    [SerializeField] private float _visionAngle;
    [SerializeField] private float _attackRange;

    [SerializeField] private EnemyWeapon _weapon;

    public EnemyPatrolState PatrolState;
    public EnemyChaseState ChaseState;
    public EnemyAttackState AttackState;
    public EnemyDeadState DeadState;

    public EnemyWeapon Weapon { get { return _weapon; } }
    public StateMachine StateMachine { get { return _stateMachine; } }
    public NavMeshAgent Agent { get { return _agent; } }
    public Transform Target { get { return _target; } }
    public Animator Animator { get { return _animator; } }
    public Health Health { get { return _health; } }
    public EnemyModel Model { get { return _model; } }
    public float PatrolRadius { get { return _patrolRadius; } }
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
        _weapon = GetComponent<EnemyWeapon>();
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






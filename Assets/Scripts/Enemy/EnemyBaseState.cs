using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState : IState
{
    protected EnemyBrain brain;
    protected NavMeshAgent agent;
    protected Transform target;
    protected Transform transform;
    protected Animator animator;
    protected EnemyModel model;
    protected Health health;

    protected float visionDistance;
    protected float visionAngle;
    protected float shootDistance;

    protected EnemyBaseState(EnemyBrain brain)
    {
        this.brain = brain;
        this.agent = brain.Agent;
        this.target = brain.Target;
        this.animator = brain.Animator;
        this.health = brain.Health;
        this.model = brain.Model;
        this.transform = brain.Transform;

        this.visionDistance = brain.VisionDistance;
        this.visionAngle = brain.VisionAngle;
        this.shootDistance = brain.ShootDistance;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

    public bool CanSeePlayer()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (direction.magnitude < visionDistance && angle < visionAngle)
        {
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = target.position - transform.position;
        if (direction.magnitude < shootDistance)
        {
            return true;
        }
        return false;
    }

    public bool IsDead()
    {
        return health.CurrentHealth <= 0;
    }
}

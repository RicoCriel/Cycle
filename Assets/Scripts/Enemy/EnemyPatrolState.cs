using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    private Vector3 _patrolCenter;
    private float _timeUntilNextPatrol;

    public EnemyPatrolState(EnemyBrain brain) : base(brain)
    {
        _patrolCenter = brain.Transform.position; 
    }

    public override void Enter()
    {
        agent.speed = 2;
        agent.isStopped = false;
        animator.SetBool("IsWalking", true);
        model.SetState(EnemyState.Patrol);

        SetRandomDestination();
    }

    public override void Update()
    {
        // If we've reached our destination or it's taking too long, get a new one
        if (agent.remainingDistance < 0.5f || agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            SetRandomDestination();
        }

        if (CanSeePlayer())
        {
            brain.StateMachine.ChangeState(brain.ChaseState);
        }

        if (IsDead())
        {
            brain.StateMachine.ChangeState(brain.DeadState);
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * brain.PatrolRadius;
        randomDirection.y = 0; 
        Vector3 randomPosition = _patrolCenter + randomDirection;

        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, brain.PatrolRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public override void Exit()
    {
        animator.SetBool("IsWalking", false);
    }


}

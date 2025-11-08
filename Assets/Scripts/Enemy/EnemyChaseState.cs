using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyBrain brain) : base(brain)
    {
    }

    public override void Enter()
    {
        model.SetState(EnemyState.Chase);
        agent.speed = 8;
        agent.isStopped = false;

        animator.SetTrigger("Running");
    }

    public override void Update()
    {
        agent.SetDestination(target.position);
        Vector3 targetPos = target.position;
        targetPos.y = 0;
        transform.LookAt(targetPos);

        if(agent.hasPath)
        {
            if (!CanSeePlayer())
            {
                brain.StateMachine.ChangeState(new EnemyPatrolState(brain));
            }

            if (CanAttackPlayer())
            {
                brain.StateMachine.ChangeState(new EnemyAttackState(brain));
            }
        }

        if(IsDead())
        {
            brain.StateMachine.ChangeState(new EnemyDeadState(brain));
        }
    }

    public override void Exit()
    {
        agent.speed = 0;
        agent.isStopped = true;
    }
}

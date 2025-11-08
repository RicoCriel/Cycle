using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyBrain brain) : base(brain)
    {
    }

    public override void Enter()
    {
        agent.speed = 0;
        agent.isStopped = true;

        model.SetState(EnemyState.Attack);
        animator.SetTrigger("Shooting");
    }

    public override void Update()
    {
        Vector3 targetPos = target.position;
        targetPos.y = 0;
        transform.LookAt(targetPos);

        if(agent.hasPath)
        {
            if (!CanAttackPlayer() && CanSeePlayer())
            {
                brain.StateMachine.ChangeState(new EnemyChaseState(brain));
            }
            else if (!CanSeePlayer())
            {
                brain.StateMachine.ChangeState(new EnemyPatrolState(brain));
            }
        }

        if (IsDead())
        {
            brain.StateMachine.ChangeState(new EnemyDeadState(brain));
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("Shooting");
        agent.speed = 0;
        agent.isStopped = true;
    }
}

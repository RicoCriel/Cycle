using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyBrain brain) : base(brain)
    {
    }

    public override void Enter()
    {
        model.SetState(EnemyState.Chase);
        agent.isStopped = false;
        agent.speed = 8;
        animator.SetBool("IsRunning", true);
    }

    public override void Update()
    {
        agent.SetDestination(target.position);
        Vector3 targetPos = target.position;
        targetPos.y = 0;
        transform.LookAt(targetPos);

        if (!CanSeePlayer())
        {
            brain.StateMachine.ChangeState(brain.PatrolState);
        }

        if (CanAttackPlayer())
        {
            brain.StateMachine.ChangeState(brain.AttackState);
        }

        if (IsDead())
        {
            brain.StateMachine.ChangeState(brain.DeadState);
        }
    }

    public override void Exit()
    {
        animator.SetBool("IsRunning", false);
    }
}

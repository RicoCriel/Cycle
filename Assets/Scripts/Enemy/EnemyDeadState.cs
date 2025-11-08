using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyBrain brain) : base(brain)
    {
    }

    public override void Enter()
    {
        agent.speed = 0;
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        model.SetState(EnemyState.Dead);
        animator.SetBool("IsDead", true);
        brain.OnDeath?.Invoke();
    }

    public override void Exit()
    {
        agent.speed = 0;
        animator.SetBool("IsDead", false);
    }

}

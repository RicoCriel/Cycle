using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyBrain brain) : base(brain)
    {
        
    }

    public override void Enter()
    {
        agent.speed = 0;
        agent.isStopped = true;

        model.SetState(EnemyState.Dead);
        animator.SetTrigger("Dead");
        brain.OnDeath?.Invoke();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        agent.speed = 0;
        animator.ResetTrigger("Dead");
    }

}

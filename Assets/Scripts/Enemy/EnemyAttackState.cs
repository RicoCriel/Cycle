using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    private float _shootCooldown = 0.75f;
    private float _nextShootTime;

    public EnemyAttackState(EnemyBrain brain) : base(brain)
    {
    }

    public override void Enter()
    {
        agent.speed = 0;
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        model.SetState(EnemyState.Attack);
        animator.SetBool("IsAttacking", true);
        _nextShootTime = Time.time;
    }

    public override void Update()
    {
        if (Time.time >= _nextShootTime)
        {
            if (brain.Weapon != null)
            {
                brain.Weapon.Shoot();
                _nextShootTime = Time.time + _shootCooldown;
            }
        }

        if (!CanAttackPlayer() && CanSeePlayer())
        {
            brain.StateMachine.ChangeState(brain.ChaseState);
        }
        else if (!CanSeePlayer())
        {
            brain.StateMachine.ChangeState(brain.PatrolState);
        }

        if (IsDead())
        {
            brain.StateMachine.ChangeState(brain.DeadState);
        }
    }

    public override void Exit()
    {
        animator.SetBool("IsAttacking", false);

    }
}

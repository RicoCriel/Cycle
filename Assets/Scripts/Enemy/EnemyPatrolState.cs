using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private int _currentIndex = -1;

    public EnemyPatrolState(EnemyBrain brain) : base(brain)
    {
    }

    public override void Enter()
    {
        agent.speed = 2;
        agent.isStopped = false;
        animator.SetBool("IsWalking", true);

        model.SetState(EnemyState.Patrol);
        float lastDistance = Mathf.Infinity;
        for (int i = 0; i < brain.EnemyPatrolPoints.Count; i++)
        {
            Transform nextWayPoint = brain.EnemyPatrolPoints[i];
            float distance = Vector3.Distance(transform.position, nextWayPoint.transform.position);
            if (distance < lastDistance)
            {
                _currentIndex = i - 1;
                lastDistance = distance;
            }
        }
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (_currentIndex >= brain.EnemyPatrolPoints.Count - 1)
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex++;
            }

            agent.SetDestination(brain.EnemyPatrolPoints[_currentIndex].transform.position);
        }


        if (CanSeePlayer())
        {
            brain.StateMachine.ChangeState(brain.ChaseState);
        }

        if(IsDead())
        {
            brain.StateMachine.ChangeState(brain.DeadState);
        }
    }

    public override void Exit()
    {
        animator.SetBool("IsWalking", false);
    }
}

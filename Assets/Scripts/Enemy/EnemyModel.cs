using UnityEngine;

public enum EnemyState
{
    Attack,
    Chase,
    Dead,
    Patrol,
}

[System.Serializable]

public class EnemyModel 
{
    public EnemyState CurrentState { get; private set; }
    public void SetState(EnemyState newState) => CurrentState = newState;
}

using UnityEngine;

public class EnemyIdleState : IState
{
    protected Enemy Enemy { get; private set; }

    protected EnemyIdleState(Enemy enemy)
    {
        Enemy = enemy;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void ExecuteLogic()
    {
    }

    public virtual void ExecutePhysic()
    {
    }
}
using UnityEngine;

public class EnemyAttackState : IState
{
    protected Enemy Enemy { get; private set; }
    protected float _timer;

    protected EnemyAttackState(Enemy enemy)
    {
        Enemy = enemy;
        _timer = Enemy.enemyData.atkSpeed;
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
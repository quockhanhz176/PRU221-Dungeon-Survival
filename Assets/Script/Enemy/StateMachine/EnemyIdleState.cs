using UnityEngine;

public class EnemyIdleState : IState
{
    private static readonly int Idle = Animator.StringToHash("idle");
    public Enemy Enemy { get; private set; }

    public EnemyIdleState(Enemy enemy)
    {
        Enemy = enemy;
    }

    public virtual void Enter()
    {
        Enemy.Anim.SetBool(Idle, true);
    }

    public virtual void Exit()
    {
        Enemy.Anim.SetBool(Idle, false);
    }

    public virtual void ExecuteLogic()
    {
    }

    public virtual void ExecutePhysic()
    {
    }
}
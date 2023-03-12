using UnityEngine;

public class EnemyChaseState : IState
{
    private static readonly int Run = Animator.StringToHash("run");

    public EnemyChaseState(Enemy enemy)
    {
        Enemy = enemy;
    }

    public Enemy Enemy { get; private set; }

    public virtual void Enter()
    {
        Enemy.Anim.SetBool(Run, true);
    }

    public virtual void Exit()
    {
        Enemy.Anim.SetBool(Run, false);
    }

    public virtual void ExecuteLogic()
    {
        // Enemy.Anim.Play("Run");
    }

    public virtual void ExecutePhysic()
    {
        // Vector2 dir = (Enemy.transform.position - Enemy.Player.transform.position).normalized;
        Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, Enemy.Player.transform.position,
            Time.deltaTime * Enemy.enemyData.moveSpeed);
    }
}
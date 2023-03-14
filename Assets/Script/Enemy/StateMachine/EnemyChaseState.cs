using UnityEngine;

public class EnemyChaseState : IState
{
    protected EnemyChaseState(Enemy enemy)
    {
        Enemy = enemy;
    }

    protected Enemy Enemy { get; private set; }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void ExecuteLogic()
    {
        // Enemy.Anim.Play("Run");
    }

    public virtual void ExecutePhysic()
    {
        // Vector2 dir = (Enemy.transform.position - Enemy.Player.transform.position).normalized;
        Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position,
            GameManager.Instance.Player.transform.position,
            Time.deltaTime * Enemy.enemyData.moveSpeed);
    }
}
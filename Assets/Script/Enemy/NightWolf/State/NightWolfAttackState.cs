using UnityEngine;

public class NightWolfAttackState : EnemyAttackState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void ExecuteLogic()
    {
        if (Vector2.Distance(Enemy.transform.position, Enemy.Player.transform.position) > Enemy.enemyData.atkRange)
        {
            Enemy.StateMachine.ChangeState(new NightWolfChaseState(Enemy));
            return;
        }

        if (!Enemy.Player.activeSelf)
        {
            Enemy.StateMachine.ChangeState(new NightWolfIdleState(Enemy));
            return;
        }

        Enemy.Sprite.flipX = (Enemy.transform.position - Enemy.Player.transform.position).normalized.x > 0;

        base.ExecuteLogic();
    }

    public override void ExecutePhysic()
    {
        base.ExecutePhysic();
    }

    public NightWolfAttackState(Enemy enemy) : base(enemy)
    {
    }
}
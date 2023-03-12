using UnityEngine;

public class NightWolfIdleState : EnemyIdleState
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
        if (Vector2.Distance(Enemy.transform.position, Enemy.Player.transform.position) <=
            Enemy.enemyData.detectionRange)
        {
            Enemy.StateMachine.ChangeState(new NightWolfChaseState(Enemy));
            return;
        }

        base.ExecuteLogic();
    }

    public override void ExecutePhysic()
    {
    }

    public NightWolfIdleState(Enemy enemy) : base(enemy)
    {
    }
}
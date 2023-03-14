using UnityEngine;

public class CursedBowmanIdleState : EnemyIdleState
{
    public CursedBowmanIdleState(Enemy enemy) : base(enemy)
    {
    }

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
        if (Vector2.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position) <=
            Enemy.enemyData.detectionRange)
        {
            Enemy.StateMachine.ChangeState(new CursedBowmanChaseState(Enemy));
            return;
        }

        if (Vector2.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position) >
            Enemy.StopAttackRange)
        {
            Enemy.StateMachine.ChangeState(new CursedBowmanAttackState(Enemy));
            return;
        }

        base.ExecuteLogic();
    }

    public override void ExecutePhysic()
    {
        base.ExecutePhysic();
    }
}
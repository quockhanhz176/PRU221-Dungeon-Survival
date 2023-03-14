using UnityEngine;

public class CursedBowmanChaseState : EnemyChaseState
{
    public CursedBowmanChaseState(Enemy enemy) : base(enemy)
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
            Enemy.enemyData.atkRange)
        {
            Enemy.StateMachine.ChangeState(new CursedBowmanAttackState(Enemy));
            return;
        }

        if (Vector2.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position) >
            Enemy.enemyData.detectionRange)
        {
            Enemy.StateMachine.ChangeState(new CursedBowmanIdleState(Enemy));
            return;
        }

        base.ExecuteLogic();
    }

    public override void ExecutePhysic()
    {
        base.ExecutePhysic();
    }
}
using UnityEngine;

public class CursedBowmanAttackState : EnemyAttackState
{
    private new float _timer;

    public CursedBowmanAttackState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        _timer = Enemy.enemyData.atkSpeed;
        Debug.Log(_timer);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void ExecuteLogic()
    {
        if (Vector2.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position) >
            Enemy.enemyData.atkRange)
        {
            Enemy.StateMachine.ChangeState(new CursedBowmanChaseState(Enemy));
            return;
        }

        if (Vector2.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position) <
            Enemy.StopAttackRange)
        {
            return;
        }

        _timer -= Time.deltaTime;
        if (!(_timer <= 0)) return;
        _timer = Enemy.enemyData.atkSpeed;
        Enemy.Attack();
        base.ExecuteLogic();
    }

    public override void ExecutePhysic()
    {
        base.ExecutePhysic();
    }
}
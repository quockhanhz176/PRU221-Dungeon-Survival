using UnityEngine;

public class CursedBowmanAttackState : EnemyAttackState
{
    private float timer;

    public CursedBowmanAttackState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        timer = Enemy.enemyData.atkSpeed;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void ExecuteLogic()
    {
        timer -= Time.deltaTime;
        if (!(timer <= 0)) return;
        timer = Enemy.enemyData.atkSpeed;
        Enemy.Attack();
    }

    public override void ExecutePhysic()
    {
        base.ExecutePhysic();
    }
}
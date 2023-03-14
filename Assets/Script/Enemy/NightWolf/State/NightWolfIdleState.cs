using UnityEngine;

public class NightWolfIdleState : EnemyIdleState
{
    private static readonly int Idle = Animator.StringToHash("idle");

    public override void Enter()
    {
        Enemy.Anim.SetBool(Idle, true);
    }

    public override void Exit()
    {
        Enemy.Anim.SetBool(Idle, false);
    }

    public override void ExecuteLogic()
    {
        if (Vector2.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position) <=
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
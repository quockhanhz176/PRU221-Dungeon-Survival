using UnityEngine;

public class NightWolfChaseState : EnemyChaseState
{
    private static readonly int Run = Animator.StringToHash("run");

    public NightWolfChaseState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Enemy.Anim.SetBool(Run, true);
    }

    public override void Exit()
    {
        Enemy.Anim.SetBool(Run, false);
    }

    public override void ExecuteLogic()
    {
        if ((Vector2.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position) >
             Enemy.enemyData.detectionRange) || !GameManager.Instance.Player.gameObject.activeSelf)
        {
            Enemy.StateMachine.ChangeState(new NightWolfIdleState(Enemy));
            return;
        }

        if (Vector2.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position) <=
            Enemy.enemyData.atkRange)
        {
            Enemy.StateMachine.ChangeState(new NightWolfAttackState(Enemy));
            return;
        }

        Enemy.Sprite.flipX = (Enemy.transform.position - GameManager.Instance.Player.transform.position).normalized.x >
                             0;

        base.ExecuteLogic();
    }

    public override void ExecutePhysic()
    {
        base.ExecutePhysic();
    }
}
using UnityEngine;

public class NightWolfAttackState : EnemyAttackState
{
    private static readonly int Attack = Animator.StringToHash("attack");

    public override void Enter()
    {
        Enemy.Anim.SetBool(Attack, true);
    }

    public override void Exit()
    {
        Enemy.Anim.SetBool(Attack, false);
    }

    public override void ExecuteLogic()
    {
        if (Vector2.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position) > Enemy.enemyData.atkRange)
        {
            Enemy.StateMachine.ChangeState(new NightWolfChaseState(Enemy));
            return;
        }

        if (!GameManager.Instance.Player.gameObject.activeSelf)
        {
            Enemy.StateMachine.ChangeState(new NightWolfIdleState(Enemy));
            return;
        }

        Enemy.Sprite.flipX = (Enemy.transform.position - GameManager.Instance.Player.transform.position).normalized.x > 0;

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer = Enemy.enemyData.atkSpeed;
            Enemy.Anim.Play("Attack");
        }

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
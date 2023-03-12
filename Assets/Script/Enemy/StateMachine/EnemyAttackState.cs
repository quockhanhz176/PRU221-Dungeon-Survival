using UnityEngine;

public class EnemyAttackState : IState
{
    private static readonly int Attack = Animator.StringToHash("attack");
    public Enemy Enemy { get; private set; }
    private float _timer;

    public EnemyAttackState(Enemy enemy)
    {
        Enemy = enemy;
        _timer = Enemy.enemyData.atkSpeed;
    }

    public virtual void Enter()
    {
        Enemy.Anim.SetBool(Attack, true);
    }

    public virtual void Exit()
    {
        Enemy.Anim.SetBool(Attack, false);
    }

    public virtual void ExecuteLogic()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer = Enemy.enemyData.atkSpeed;
            Enemy.Anim.Play("Attack");
        }
    }

    public virtual void ExecutePhysic()
    {
    }
}
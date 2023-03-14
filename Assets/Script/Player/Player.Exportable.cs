using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Player : IExportable<PlayerData>
{
    private Health _health;

    public PlayerData Export()
    {
        return new PlayerData
        {
            Position = transform.position,
            LookDirection = LookDirection,
            CurrentHealth = _health.CurrentHealth,
            BasicShootData = BasicShoot.Export(),
            DashData = DashSkill.Export(),
            PickupableSkillData = PickupableSkill == null ? null : (PickupableSkillData)PickupableSkill.Export(),
        };
    }

    public void Import(PlayerData o)
    {
        transform.position = o.Position;
        LookDirection = o.LookDirection;
        var currentHealth = _health.CurrentHealth;
        if (o.CurrentHealth > currentHealth)
        {
            _health.ReceiveHealing(o.CurrentHealth - currentHealth);
        }
        else if (o.CurrentHealth < currentHealth)
        {
            _health.TakeDamage(currentHealth - o.CurrentHealth);
        }
        BasicShoot.Import(o.BasicShootData);
        DashSkill.Import(o.DashData);
        var pSkill = o.PickupableSkillData?.PickupableSkill;
        if (pSkill != null)
        {
            SubmitPickupableSkill((PickupableSkill)pSkill);
            PickupableSkill.Import(o.PickupableSkillData);
        }
    }
}

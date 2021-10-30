using UnityEngine;

public class BossLevel : EnemyLevel
{
    [Header("Boss")]
    [SerializeField] protected int hpMulti = 2;


    public override int Set(int newLevel)
    {
        base.Set(newLevel);

        int newMaxHp = this.level + 2;
        newMaxHp *= this.hpMulti;

        this.enemyCtrl.damageReceiver.SetHPMax(newMaxHp);
        this.enemyCtrl.damageReceiver.SetHP(newMaxHp);

        return this.level;
    }
}

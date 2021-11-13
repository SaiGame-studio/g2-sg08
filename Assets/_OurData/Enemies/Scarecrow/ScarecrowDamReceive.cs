using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowDamReceive : DamageReceiver
{
    [Header("Enemy")]
    public EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = GetComponent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadEnemyCtrl");
    }

    public override void Receive(int damage)
    {
        this.enemyCtrl.scaleSpring.enabled = true;
        base.Receive(damage);
    }

    public override void Despawn()
    {
        this.ShowDeadEffect();
        ObjPoolManager.instance.Despawn(transform);

        ScoreManager.instance.Kill();

        int gold = ScarecrowSpawner.instance.GoldOnDead();
        TextManager.instance.TextGold(gold, transform.position);
        ScoreManager.instance.GoldAdd(gold);

        ScarecrowSpawner.instance.ScarecrowDead();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageSender : DamageSender
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

    protected override void SendDamage(GameObject coliderObj)
    {
        DamageReceiver damageReveiver = coliderObj.GetComponent<DamageReceiver>();
        if (damageReveiver == null) return;

        int currentHP = this.enemyCtrl.damageReceiver.HP();
        damageReveiver.Receive(1, this);
        this.enemyCtrl.damageReceiver.Receive(currentHP);
    }
}

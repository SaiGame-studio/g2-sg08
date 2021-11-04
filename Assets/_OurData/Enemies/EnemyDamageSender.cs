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

        damageReveiver.Receive(1, this);

        //int currentHP = this.enemyCtrl.damageReceiver.HP();
        //this.enemyCtrl.damageReceiver.Receive(currentHP);

        this.enemyCtrl.damageReceiver.Despawn();
    }
}

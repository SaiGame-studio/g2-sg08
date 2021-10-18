using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowDamReceive : EnemyDamageReceiver
{
    protected override void Despawn()
    {
        base.Despawn();
        ScarecrowSpawner.instance.ScarecrowDead();
    }
}

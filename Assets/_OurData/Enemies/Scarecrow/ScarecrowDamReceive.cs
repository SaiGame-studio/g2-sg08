using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowDamReceive : EnemyDamageReceiver
{
    public override void Despawn()
    {
        base.Despawn();
        ScarecrowSpawner.instance.ScarecrowDead();
    }
}

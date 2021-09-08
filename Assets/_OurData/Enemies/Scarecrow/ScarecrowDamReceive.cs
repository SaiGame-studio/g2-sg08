using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowDamReceive : DamageReceiver
{
    protected override void Despawn()
    {
        base.Despawn();
        ScarecrowSpawner.instance.ScarecrowDead();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : SaiBehaviour
{
    [SerializeField] protected float limit = 2;
    [SerializeField] protected float timer = 0;

    private void FixedUpdate()
    {
        this.Checking();
    }

    protected virtual void Checking()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.limit) return;
        this.timer = 0;

        this.Despawn();
    }

    protected virtual void Despawn()
    {
        ObjPoolManager.instance.Despawn(transform);
    }
}

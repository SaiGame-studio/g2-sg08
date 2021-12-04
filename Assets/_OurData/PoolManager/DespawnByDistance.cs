using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : SaiBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float distance = 0;
    [SerializeField] protected float distanceLimit = 70;

    private void FixedUpdate()
    {
        this.Checking();
    }

    protected virtual void Checking()
    {
        this.LoadTarget();
        this.distance = Vector3.Distance(transform.position, this.target.position);
        if (this.distance < this.distanceLimit) return;

        this.Despawn();
    }

    protected virtual void LoadTarget()
    {
        this.target = PlayerManager.instance.currentHero.transform;
    }

    protected virtual void Despawn()
    {
        ObjPoolManager.instance.Despawn(transform);
    }
}

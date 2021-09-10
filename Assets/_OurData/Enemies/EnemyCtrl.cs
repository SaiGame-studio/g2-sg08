using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : SaiBehaviour
{
    [Header("Enemy")]
    public Rigidbody _rigidbody;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigibody();
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigibody");
    }
}

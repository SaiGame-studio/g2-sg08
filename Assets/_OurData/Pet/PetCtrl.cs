using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCtrl : SaiBehaviour
{
    public Rigidbody _rigidbody;
    public Transform model;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigibody();
        this.LoadModel();
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        Debug.LogWarning(transform.name + ": LoadModel");
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();

        this._rigidbody.useGravity = false;

        this._rigidbody.constraints = RigidbodyConstraints.FreezePositionZ
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezeRotationZ;

        Debug.LogWarning(transform.name + ": LoadRigibody");
    }
}

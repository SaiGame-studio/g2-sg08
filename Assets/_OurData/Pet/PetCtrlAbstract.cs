using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetCtrlAbstract : SaiBehaviour
{
    [Header("Pet Ctrl Abstract")]
    public PetCtrl petCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPetCtrl();
    }

    protected virtual void LoadPetCtrl()
    {
        if (this.petCtrl != null) return;
        this.petCtrl = transform.parent.GetComponent<PetCtrl>();
        Debug.LogWarning(transform.name + ": LoadPetCtrl", gameObject);
    }
}

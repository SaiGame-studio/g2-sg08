using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueInteractable : PlayerInteractable
{
    [Header("Statue")]
    public StatueCtrl statueCtrl;

    protected override void LoadComponents()
    {
        this.LoadStatueCtrl();
    }

    protected virtual void LoadStatueCtrl()
    {
        if (this.statueCtrl != null) return;
        this.statueCtrl = GetComponent<StatueCtrl>();

        Debug.Log(transform.name + ": LoadStatueCtrl");
    }

    public override void Interact()
    {
        this.statueCtrl.statueLevel.Up(1);
    }
}

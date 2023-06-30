using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : SaiBehaviour
{
    public static PetManager instance;
    public PetCtrl currentPet;

    private void Awake()
    {
        if (PetManager.instance != null) Debug.LogError("Only 1 PetManager allow");
        PetManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFirstPet();
    }

    protected virtual void LoadFirstPet()
    {
        if (this.currentPet != null) return;
        this.currentPet = GameObject.FindObjectOfType<PetCtrl>();
        Debug.LogWarning(transform.name + ": LoadFirstPet", gameObject);
    }
}

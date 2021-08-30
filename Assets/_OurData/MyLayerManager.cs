using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLayerManager : SaiBehaviour
{
    public static MyLayerManager instance;

    [Header("Layers")]
    public int layerHero;
    public int layerGround;
    public int layerCeiling;

    private void Awake()
    {
        if (MyLayerManager.instance != null) Debug.LogError("Only 1 MyLayerManager allow");
        MyLayerManager.instance = this;
        this.LoadComponents();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetPlayers();
    }

    protected virtual void GetPlayers()
    {
        if (this.layerHero > 0 && this.layerGround > 0 && this.layerCeiling > 0) return;

        this.layerHero = LayerMask.NameToLayer("Hero");
        this.layerGround = LayerMask.NameToLayer("Ground");
        this.layerCeiling = LayerMask.NameToLayer("Ceiling");

        if (this.layerHero < 0) Debug.LogError("Layer Hero is mising");
        if (this.layerGround < 0) Debug.LogError("Layer Ground is mising");
        if (this.layerCeiling < 0) Debug.LogError("Layer Ceiling is mising");

        Debug.Log(transform.name + ": GetPlayers");
    }
}

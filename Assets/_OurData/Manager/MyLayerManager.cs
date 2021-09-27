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
    public int layerEnemy;
    public int layerBullet;
    public int layerStatue;
    public int layerStatueBroken;

    private void Awake()
    {
        if (MyLayerManager.instance != null) Debug.LogError("Only 1 MyLayerManager allow");
        MyLayerManager.instance = this;

        this.LoadComponents();

        Physics.IgnoreLayerCollision(this.layerHero, this.layerHero, true);
        Physics.IgnoreLayerCollision(this.layerHero, this.layerEnemy, true);
        Physics.IgnoreLayerCollision(this.layerHero, this.layerBullet, true);
        Physics.IgnoreLayerCollision(this.layerEnemy, this.layerEnemy, true);
        //Physics.IgnoreLayerCollision(this.layerEnemy, this.layerStatueBroken, true);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetPlayers();
    }

    protected virtual void GetPlayers()
    {
        if (this.layerHero > 0 && this.layerGround > 0 && this.layerCeiling > 0 && this.layerEnemy > 0) return;

        this.layerHero = LayerMask.NameToLayer("Hero");
        this.layerGround = LayerMask.NameToLayer("Ground");
        this.layerCeiling = LayerMask.NameToLayer("Ceiling");
        this.layerEnemy = LayerMask.NameToLayer("Enemy");
        this.layerBullet = LayerMask.NameToLayer("Bullet");
        this.layerStatue = LayerMask.NameToLayer("Statue");
        this.layerStatueBroken = LayerMask.NameToLayer("StatueBroken");

        if (this.layerHero < 0) Debug.LogError("Layer Hero is mising");
        if (this.layerGround < 0) Debug.LogError("Layer Ground is mising");
        if (this.layerCeiling < 0) Debug.LogError("Layer Ceiling is mising");
        if (this.layerBullet < 0) Debug.LogError("Layer Bullet is mising");
        if (this.layerStatue < 0) Debug.LogError("Layer Statue is mising");
        if (this.layerStatueBroken < 0) Debug.LogError("Layer StatueBroken is mising");

        Debug.Log(transform.name + ": GetPlayers");
    }

    //void GetSpeedRandom()
    //{
    //    List<float> speedList = new List<float>();
    //    speedList.Add(10);
    //    speedList.Add(20);
    //    speedList.Add(30);
    //    speedList.Add(40);

    //    int rand = Random.Range(0, speedList.Count - 1);
    //    float finalSpeed = speedList[rand];
    //}
}

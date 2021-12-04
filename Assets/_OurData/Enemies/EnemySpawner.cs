using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public static EnemySpawner instance;

    [Header("Enemy")]
    [SerializeField] protected List<string> nameEnemies;
    [SerializeField] protected Transform target;

    private void Awake()
    {
        if (EnemySpawner.instance != null) Debug.LogError("Only 1 EnemySpawner allow");
        EnemySpawner.instance = this;
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.nameEnemies.Add("Larva");
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTarget();
    }

    protected override Vector3 SpawnPos()
    {
        Transform pos = SpawnPosManager.instance.GetPos(0);

        return pos.position;
    }

    protected override void BeforeSpawn()
    {
        this.enemyName = this.GetEnemyName();
    }

    protected override void AfterSpawn(Transform obj)
    {
        EnemyCtrl enemyCtrl = obj.GetComponent<EnemyCtrl>();
        enemyCtrl.enemyMovement.SetTarget(this.target);

        int gameLevel = GameLevelManager.instance.Level();
        enemyCtrl.enemyLevel.Set(gameLevel);
    }

    protected virtual string GetEnemyName()
    {
        return this.nameEnemies[0];
    }

    protected override int SpwamnLimit()
    {
        int level = GameLevelManager.instance.Level();
        this.finalSpawnLimit = this.spawnLimit * level;
        if (this.finalSpawnLimit > this.spawmMax) this.finalSpawnLimit = this.spawmMax;
        return this.finalSpawnLimit;
    }

    protected override float SpawnDelay()
    {
        int level = GameLevelManager.instance.Level();
        this.finalSpawnDelay = this.spawnDelay - (level * 0.1f);
        if (this.finalSpawnDelay < 0.2f) this.finalSpawnDelay = 0.2f;
        return this.finalSpawnDelay;
    }

    protected virtual void LoadTarget()
    {
        if (this.target != null) return;
        this.target = GameObject.Find("EnemyGate1").transform;

        Debug.Log(transform.name + ": LoadTarget");
    }

    public virtual Transform RandomEnemy()
    {
        int rand = Random.Range(0, this.objests.Count);
        return this.objests[rand];
    }
}

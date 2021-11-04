using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : Spawner
{
    public static BossSpawner instance;

    [Header("Boss")]
    [SerializeField] protected List<string> nameEnemies;
    [SerializeField] protected int lastSpawnLevel = 0;
    [SerializeField] protected int spawnLevel = 7;
    [SerializeField] protected bool canSpawn = false;

    private void Awake()
    {
        if (BossSpawner.instance != null) Debug.LogError("Only 1 BossSpawner allow");
        BossSpawner.instance = this;
    }

    private void FixedUpdate()
    {
        this.CheckCanSpawn();
        this.Spawning();
        this.RemoveDespawn();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.nameEnemies.Add("Boss1");
        this.spawnLimit = 1;
        this.spawnDelay = 2;
        this.spawnLimitOrigin = 1;
    }

    protected override void BeforeSpawn()
    {
        this.enemyName = this.GetEnemyName();
    }

    protected override void AfterSpawn(Transform obj)
    {
        EnemyCtrl enemyCtrl = obj.GetComponent<EnemyCtrl>();

        int gameLevel = GameLevelManager.instance.Level() + 20;
        enemyCtrl.enemyLevel.Set(gameLevel);

        Vector3 position = enemyCtrl.transform.position;

        Transform effect = ObjPoolManager.instance.Spawn("EnemyDeath1", position);
        effect.gameObject.SetActive(true);

        this.canSpawn = false;
    }

    protected virtual string GetEnemyName()
    {
        return this.nameEnemies[0];
    }

    protected override Vector3 SpawnPos()
    {
        Transform pos = SpawnPosManager.instance.GetPos(0);
        Vector3 position = pos.position;
        position.x += 5;

        return position;
    }

    protected virtual void CheckCanSpawn()
    {
        int limit = GameLevelManager.instance.Level();
        if (limit < this.spawnLevel) return;
        if (limit == this.lastSpawnLevel) return;

        float remainder = limit % this.spawnLevel;
        if (remainder == 0)
        {
            this.canSpawn = true;
            this.lastSpawnLevel = limit;
        }
    }

    protected override bool CanSpawn()
    {
        int childCount = this.CountActiveObject();

        if (childCount >= this.SpwamnLimit()) return false;

        return this.canSpawn;
    }

    protected override int SpwamnLimit()
    {
        return this.spawnLimit;
    }

    protected override float SpawnDelay()
    {
        return this.spawnDelay;
    }

    public override void GameRenew()
    {
        base.GameRenew();
        this.canSpawn = false;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [Header("Enemy")]
    [SerializeField] protected List<Transform> spawnPos;
    [SerializeField] protected List<string> nameEnemies;
    [SerializeField] protected Transform target;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.nameEnemies.Add("Larva");
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPos();
    }

    protected virtual void LoadSpawnPos()
    {
        if (this.spawnPos.Count > 0) return;
        foreach (Transform child in transform)
        {
            this.spawnPos.Add(child);
            child.gameObject.SetActive(false);
        }
        Debug.Log(transform.name + ": LoadSpawnPos");
    }

    protected override Vector3 SpawnPos()
    {
        int rand = Random.Range(0, this.spawnPos.Count);
        Transform pos = this.spawnPos[rand];

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
    }

    protected virtual string GetEnemyName()
    {
        return this.nameEnemies[0];
    }

    protected override int SpwamnLimit()
    {
        int level = GameLevelManager.instance.Level();
        this.finalSpawnLimit = this.spawnLimit * level;
        return this.finalSpawnLimit;
    }

    protected override float SpawnDelay()
    {
        int level = GameLevelManager.instance.Level();
        this.finalSpawnDelay = this.spawnDelay - (level * 0.1f);
        if (this.finalSpawnDelay < 0.2f) this.finalSpawnDelay = 0.2f;
        return this.finalSpawnDelay;
    }
}

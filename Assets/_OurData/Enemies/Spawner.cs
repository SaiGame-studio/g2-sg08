using System.Collections.Generic;
using UnityEngine;

public class Spawner : SaiBehaviour
{
    [Header("Spawner")]
    [SerializeField] protected string enemyName = "Cube";
    [SerializeField] protected int spawnLimitOrigin = 2;
    [SerializeField] protected int spawnLimit = 2;
    [SerializeField] protected int finalSpawnLimit = 2;
    [SerializeField] protected float spawnDelayOrigin = 2;
    [SerializeField] protected float spawnDelay = 2;
    [SerializeField] protected float finalSpawnDelay = 2;
    [SerializeField] protected float spawnTimer = Mathf.Infinity;
    public List<Transform> objests = new List<Transform>();

    private void FixedUpdate()
    {
        this.Spawning();
        this.RemoveDespawn();
    }

    protected virtual void Spawning()
    {
        if (GameLevelManager.instance.gameOver) return;
        if (!this.CanSpawn()) return;

        this.spawnTimer += Time.fixedDeltaTime;
        if (this.spawnTimer < this.SpawnDelay()) return;
        this.spawnTimer = 0;

        this.BeforeSpawn();
        Transform obj = ObjPoolManager.instance.Spawn(this.EnemyName(), this.SpawnPos(), transform.rotation, transform);
        this.objests.Add(obj);

        this.AfterSpawn(obj);
        obj.gameObject.SetActive(true);
    }

    protected virtual void RemoveDespawn()
    {
        for (int i = 0; i < this.objests.Count; i++)
        {
            Transform obj = this.objests[i];
            if (obj.gameObject.activeSelf) continue;

            this.objests.Remove(obj);
        }
    }

    protected virtual string EnemyName()
    {
        return this.enemyName;
    }

    protected virtual void BeforeSpawn()
    {
        //Nothing to do
    }

    protected virtual void AfterSpawn(Transform obj)
    {
        //Nothing to do
    }

    protected virtual Vector3 SpawnPos()
    {
        float x = Random.Range(-7.0f, 7.0f);
        float y = Random.Range(7.0f, 9.0f);

        return new Vector3(x, y, 0);
    }

    protected virtual bool CanSpawn()
    {
        int childCount = this.CountActiveObject();

        if (childCount >= this.SpwamnLimit()) return false;
        return true;
    }

    protected virtual int SpwamnLimit()
    {
        this.finalSpawnLimit = this.spawnLimit;
        return this.finalSpawnLimit;
    }

    protected virtual float SpawnDelay()
    {
        this.finalSpawnDelay = this.spawnDelay;
        return this.finalSpawnDelay;
    }

    protected virtual int CountActiveObject()
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf) count++;
        }

        return count;
    }

    public virtual void GameRenew()
    {
        Transform obj;
        for (int i = 0; i < this.objests.Count; i++)
        {
            obj = this.objests[i];
            ObjPoolManager.instance.Despawn(obj);
        }

        this.ResetNumber();
    }

    protected virtual void ResetNumber()
    {
        this.spawnDelay = this.spawnDelayOrigin;
        this.spawnLimit = this.spawnLimitOrigin;
    }
}

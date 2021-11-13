using UnityEngine;

public class ScarecrowSpawner : Spawner
{
    public static ScarecrowSpawner instance;

    [Header("Scarecrow")]
    [SerializeField] protected float height = 4.3f;

    private void Awake()
    {
        if (ScarecrowSpawner.instance != null) Debug.LogError("Only 1 ScarecrowSpawner allow");
        ScarecrowSpawner.instance = this;
    }

    protected override void ResetValue()
    {
        this.enemyName = "Scarecrow";
        this.spawnLimit = 1;
        this.spawnLimitOrigin = 1;
    }

    protected override Vector3 SpawnPos()
    {
        float x = Random.Range(-7.0f, 7.0f);
        Vector3 spawnPos = new Vector3(x, this.height, 0);
        return spawnPos;
    }

    public virtual void ScarecrowDead()
    {
        this.spawnDelay += 2;
    }

    public virtual int GoldOnDead()
    {
        int gold = (int)this.spawnDelay * 2;
        return gold;
    }
}

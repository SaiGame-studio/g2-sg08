using UnityEngine;

public class EnemySpawner : SaiBehaviour
{
    [Header("Enemy")]
    [SerializeField] protected string enemyName = "Cube";
    [SerializeField] protected int spawnLimit = 2;
    [SerializeField] protected float spawnDelay = 2;

    private void Start()
    {
        this.Spawning();
    }

    protected virtual void Spawning()
    {
        Invoke("Spawning", this.spawnDelay);

        if (!this.CanSpawn()) return;

        Vector3 spawnPos = this.SpawnPos();

        Transform obj = ObjPoolManager.instance.Spawn(this.enemyName, spawnPos, transform.rotation, transform);
        obj.gameObject.SetActive(true);
    }

    protected virtual Vector3 SpawnPos()
    {
        float x = Random.Range(-7.0f, 7.0f);
        float y = Random.Range(7.0f, 9.0f);

        Vector3 spawnPos = new Vector3(x, y, 0);

        return spawnPos;
    }

    protected virtual bool CanSpawn()
    {
        int childCount = this.CountActiveObject();

        if (childCount >= this.spawnLimit) return false;
        return true;
    }

    protected virtual int CountActiveObject()
    {
        int count = 0;
        foreach(Transform child in transform)
        {
            if (child.gameObject.activeSelf) count++;
        }

        return count;
    }
}

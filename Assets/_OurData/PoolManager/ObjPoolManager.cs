using PathologicalGames;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager : SaiBehaviour
{
    public static ObjPoolManager instance;
    [SerializeField] protected string poolName = "ObjPool";
    [SerializeField] protected SpawnPool pool;
    [SerializeField] protected List<Transform> objs;
    [SerializeField] protected List<PrefabPool> prefabPools;

    private void Awake()
    {
        if (ObjPoolManager.instance != null) Debug.LogError("Only 1 ObjPoolManager allow");
        ObjPoolManager.instance = this;

        this.AddObjToPool();
    }

    protected override void LoadComponents()
    {
        this.LoadPool();
        this.LoadObjs();
    }

    protected virtual void LoadPool()
    {
        if (this.pool != null) return;

        GameObject obj = GameObject.Find(this.poolName);
        this.pool = obj.GetComponent<SpawnPool>();
        this.pool.poolName = this.poolName;
        Debug.Log(transform.name + ": LoadPool");
    }

    protected virtual void LoadObjs()
    {
        if (this.objs.Count > 0) return;
        foreach (Transform child in transform)
        {
            this.objs.Add(child);
            child.gameObject.SetActive(false);
        }
        Debug.Log(transform.name + ": LoadObjs");
    }

    protected virtual void AddObjToPool()
    {
        foreach (Transform obj in this.objs)
        {
            PrefabPool prefabPool = new PrefabPool(obj)
            {
                preloadAmount = 2
            };

            bool isAlreadyPool = this.pool.GetPrefabPool(prefabPool.prefab) == null ? false : true;
            if (!isAlreadyPool)
            {
                this.pool.CreatePrefabPool(prefabPool);
                this.prefabPools.Add(prefabPool);
            }
        }
    }

    public virtual SpawnPool Pool()
    {
        this.LoadPool();
        return this.pool;
    }

    public virtual Transform Spawn(string objName, Transform parent)
    {
        return this.Pool().Spawn(objName, parent);
    }

    public virtual Transform Spawn(string objName, Vector3 pos, Quaternion rot)
    {
        return this.Pool().Spawn(objName, pos, rot);
    }

    public virtual Transform Spawn(string objName, Vector3 pos, Quaternion rot, Transform parent)
    {
        return this.Pool().Spawn(objName, pos, rot, parent);
    }

    public virtual void Despawn(Transform instance)
    {
        for (int i = 0; i < this.prefabPools.Count; i++)
        {
            PrefabPool prefabPool = this.prefabPools[i];
            if (prefabPool.despawned.Contains(instance))
            {
                Debug.LogWarning("despawned.Contains: "+ instance.name);
                return;
            }
        }

        this.Pool().Despawn(instance);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosManager : SaiBehaviour
{
    public static SpawnPosManager instance;

    [Header("Spawn Pos")]
    [SerializeField] protected List<Transform> spawnPos;

    private void Awake()
    {
        if (SpawnPosManager.instance != null) Debug.LogError("Only 1 SpawnPosManager allow");
        SpawnPosManager.instance = this;
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
        }
        Debug.Log(transform.name + ": LoadSpawnPos");
    }

    public virtual Transform GetPos(int index)
    {
        return this.spawnPos[index];
    }

}

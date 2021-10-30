using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : SaiBehaviour
{
    [Header("Boss Skill")]
    [SerializeField] protected EnemyCtrl target;
    [SerializeField] protected float spawnDelay = 10;
    [SerializeField] protected float spawnTimer = 0;

    private void FixedUpdate()
    {
        this.Attacking();
    }

    protected virtual void Attacking()
    {
        this.spawnTimer += Time.fixedDeltaTime;
        if (this.spawnTimer < this.spawnDelay) return;
        this.spawnTimer = 0;

        Debug.Log("Attacking");
        //this.target = EnemySpawner.instance.RandomEnemy();
    }

}

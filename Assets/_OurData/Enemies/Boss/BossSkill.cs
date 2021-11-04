using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : SaiBehaviour
{
    [Header("Boss Skill")]
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected Transform target;
    [SerializeField] protected float buffSpeed = 2;
    [SerializeField] protected float spawnDelay = 5;
    [SerializeField] protected float spawnTimer = 0;

    private void FixedUpdate()
    {
        this.Attacking();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();

        Debug.Log(transform.name + ": LoadEnemyCtrl");
    }

    protected virtual void Attacking()
    {
        this.spawnTimer += Time.fixedDeltaTime;
        if (this.spawnTimer < this.spawnDelay) return;
        this.spawnTimer = 0;

        this.target = EnemySpawner.instance.RandomEnemy();
        EnemyCtrl enemyCtrl = this.target.GetComponent<EnemyCtrl>();

        float newSpeed = enemyCtrl.enemyMovement.GetSpeed() + this.buffSpeed;
        enemyCtrl.enemyMovement.SetSpeed(newSpeed);

        Transform effect = ObjPoolManager.instance.Spawn("EnemyDeath1", this.target.position);
        effect.gameObject.SetActive(true);

        this.enemyCtrl.monster.Animator.SetBool("Action", true);
        this.enemyCtrl.monster.Attack();
        Invoke("ResetAction", 1f);
    }

    protected virtual void ResetAction()
    {
        this.enemyCtrl.monster.Animator.SetBool("Action", false);
    }
}

﻿using UnityEngine;

public class EnemyMovement : SaiBehaviour
{
    [Header("Enemy")]
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected Transform target;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected Vector3 direction = new Vector3(0, 0, 0);

    private void Update()
    {
        this.Moving();
    }

    private void FixedUpdate()
    {
        this.Turning();
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

    protected virtual void Moving()
    {
        if (!this.IsTargetActive()) return;

        Vector3 tempVec = this.GetDirection() * Time.deltaTime * this.speed;
        this.enemyCtrl._rigidbody.MovePosition(transform.position + tempVec);
    }

    protected virtual Vector3 GetDirection()
    {
        this.direction.x = 0;
        if (this.target.position.x > transform.position.x) this.direction.x = 1;
        if (this.target.position.x < transform.position.x) this.direction.x = -1;
        return this.direction;
    }

    protected virtual bool IsTargetActive()
    {
        if (this.target == null) return false;

        return true;
    }

    protected virtual void Turning()
    {
        Vector3 newScale = this.enemyCtrl.enemy.transform.localScale;
        newScale.x = Mathf.Abs(newScale.x);
        newScale.x *= this.direction.x * -1;
        this.enemyCtrl.enemy.transform.localScale = newScale;
    }
}
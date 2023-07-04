using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAttack : PetCtrlAbstract
{
    [Header("Pet Attack")]
    [SerializeField] protected Transform originMoveTarget;
    [SerializeField] protected Transform target;
    [SerializeField] protected DamageReceiver targetDamRecevier;
    [SerializeField] protected bool goingBack2Player = false;
    [SerializeField] protected float attackRange = 3f;
    [SerializeField] protected float attackSpeed = 1f;
    [SerializeField] protected float attackTimer;
    [SerializeField] protected float targetDis = Mathf.Infinity;
    [SerializeField] protected Vector3 targetDir;

    private void FixedUpdate()
    {
        this.TargetFinding();
        this.CheckTargetIsDead();
        this.CheckIsNearPlayer();
        this.AttackTarget();
    }

    private void OnDrawGizmos()
    {
        this.ShowTargetZone();
    }

    protected virtual void CheckTargetIsDead()
    {
        if (this.targetDamRecevier == null) return;
        if (!this.TargetIsDead()) return;

        this.ResetTarget();
    }

    protected virtual bool TargetIsDead()
    {
        if (this.targetDamRecevier.IsDead()) return true;
        if (this.targetDamRecevier.gameObject.activeSelf == false) return true;
        return false;
    }

    protected virtual void CheckIsNearPlayer()
    {
        if (!this.goingBack2Player) return;
        if (this.originMoveTarget != this.petCtrl.petMovement.Target) return;

        bool isNear = this.petCtrl.petMovement.CurrentDis <= this.petCtrl.petMovement.LimitDis;
        if (isNear) this.goingBack2Player = false;
    }

    protected virtual void ResetTarget()
    {
        this.petCtrl.petMovement.SetTarget(this.originMoveTarget);
        this.target = null;
        this.targetDamRecevier = null;
        //this.originMoveTarget = null;
        this.goingBack2Player = true;
        //Debug.LogError("Stop ResetTarget");
    }


    protected virtual void AttackTarget()
    {
        
    }


    protected virtual void TargetFinding()
    {
        if (this.target) return;
        if (this.goingBack2Player) return;

        float distance;
        foreach (Transform obj in EnemySpawner.instance.objests)
        {
            distance = Vector3.Distance(transform.position, obj.position);
            if (distance <= this.attackRange)
            {
                this.TargetSet(obj);
                return;
            }
        }
    }

    protected virtual void TargetSet(Transform target)
    {
        this.targetDamRecevier = target.GetComponent<DamageReceiver>();
        this.originMoveTarget = this.petCtrl.petMovement.Target;
        this.petCtrl.petMovement.SetTarget(target);

        this.target = target;
    }

    protected virtual void ShowTargetZone()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, this.attackRange);
    }
}

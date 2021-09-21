using UnityEngine;

public class EnemyCtrl : SaiBehaviour
{
    [Header("Enemy")]
    public Rigidbody _rigidbody;
    public Transform enemy;
    public EnemyMovement enemyMovement;
    public DamageReceiver damageReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigibody();
        this.LoadEnemy();
        this.LoadEnemyReceiver();
    }

    protected virtual void LoadEnemyReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = GetComponent<DamageReceiver>();

        Debug.Log(transform.name + ": LoadEnemyReceiver");
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();

        this._rigidbody.constraints = RigidbodyConstraints.FreezePositionZ
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezeRotationZ;

        Debug.Log(transform.name + ": LoadRigibody");
    }

    protected virtual void LoadEnemy()
    {
        if (this.enemy != null) return;
        this.enemy = transform.Find("Enemy");
        this.enemyMovement = transform.Find("EnemyMovement").GetComponent<EnemyMovement>();
        Debug.Log(transform.name + ": LoadEnemy");
    }
}

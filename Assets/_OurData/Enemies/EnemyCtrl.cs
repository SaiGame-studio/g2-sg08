using UnityEngine;

public class EnemyCtrl : SaiBehaviour
{
    [Header("Enemy")]
    public Rigidbody _rigidbody;
    public Transform enemy;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigibody();
        this.LoadEnemy();
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigibody");
    }

    protected virtual void LoadEnemy()
    {
        if (this.enemy != null) return;
        this.enemy = transform.Find("Enemy");
        Debug.Log(transform.name + ": LoadEnemy");
    }
}

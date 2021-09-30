using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [Header("Enemy")]
    public EnemyCtrl enemyCtrl;

    private void OnEnable()
    {
        this.OnRenew();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = GetComponent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadEnemyCtrl");
    }

    protected override void Despawn()
    {
        base.Despawn();
        this.enemyCtrl._collider.enabled = false;
    }

    protected virtual void OnRenew()
    {
        this.enemyCtrl._collider.enabled = true;
    }
}

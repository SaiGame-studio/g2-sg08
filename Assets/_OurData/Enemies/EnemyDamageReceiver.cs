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

    public override void Receive(int damage)
    {
        this.enemyCtrl.scaleSpring.enabled = true;
        base.Receive(damage);
        TextManager.instance.TextGold(damage, transform.position);
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = GetComponent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadEnemyCtrl");
    }

    protected override void Dying()
    {
        if (!this.IsDead()) return;

        base.Dying();

        ScoreManager.instance.Kill();
        ScoreManager.instance.GoldAdd(1);
    }

    public override void Despawn()
    {
        base.Despawn();
        this.enemyCtrl._collider.enabled = false;
    }

    protected virtual void OnRenew()
    {
        this.enemyCtrl._collider.enabled = true;
    }
}

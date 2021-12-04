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

        ScoreManager.instance.Kill();

        int gold = Mathf.RoundToInt(this.hpMax / 5);
        if (gold < 1) gold = 1;
        TextManager.instance.TextGold(gold, transform.position);
        ScoreManager.instance.GoldAdd(gold);

        base.Dying();
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

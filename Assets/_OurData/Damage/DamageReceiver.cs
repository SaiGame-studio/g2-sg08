using UnityEngine;

public class DamageReceiver : SaiBehaviour
{
    [Header("DamageReceiver")]
    [SerializeField] protected int hpMax = 2;
    [SerializeField] protected int hp = 0;
    [SerializeField] protected string deadEffect = "EnemyDeath1";

    private void Star()
    {
        this.Revival();
    }

    private void OnEnable()
    {
        this.Revival();
    }

    protected virtual void Revival()
    {
        this.hp = this.hpMax;
    }

    public virtual bool IsDead()
    {
        return this.hp <= 0;
    }

    public virtual void Receive(int damage)
    {
        this.hp -= damage;

        this.Dying();
    }

    protected virtual void Dying()
    {
        if (!this.IsDead()) return;

        this.ShowDeadEffect();
        this.Despawn();
    }

    protected virtual void ShowDeadEffect()
    {
        Transform effect = ObjPoolManager.instance.Spawn(this.deadEffect, transform.position, transform.rotation);
        effect.gameObject.SetActive(true);
    }

    protected virtual void Despawn()
    {
        ObjPoolManager.instance.Despawn(transform);
    }
}

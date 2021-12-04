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

    public virtual bool IsHPFull()
    {
        return this.hp == this.hpMax;
    }

    public virtual int HP()
    {
        return this.hp;
    }

    public virtual void Revival()
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

    public virtual void Receive(int damage, DamageSender sender)
    {
        this.Receive(damage);
    }

    protected virtual void Dying()
    {
        if (!this.IsDead()) return;

        this.Despawn();
    }

    protected virtual void ShowDeadEffect()
    {
        Transform effect = ObjPoolManager.instance.Spawn(this.deadEffect, transform.position);
        effect.gameObject.SetActive(true);
    }

    public virtual void Despawn()
    {
        this.ShowDeadEffect();
        ObjPoolManager.instance.Despawn(transform);
    }

    public virtual bool Heal()
    {
        this.hp = this.hpMax;

        return true;
    }

    public virtual void SetHPMax(int newHPMax)
    {
        this.hpMax = newHPMax;
    }

    public virtual void SetHP(int newHP)
    {
        this.hp = newHP;
    }
}

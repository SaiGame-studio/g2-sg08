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

    public virtual void Receive(int damage, DamageSender sender)
    {
        this.Receive(damage);
    }

    protected virtual void Dying()
    {
        if (!this.IsDead()) return;

        this.ShowDeadEffect();
        this.Despawn();
    }

    protected virtual void ShowDeadEffect()
    {
        Transform effect = ObjPoolManager.instance.Spawn(this.deadEffect, transform.position);
        effect.gameObject.SetActive(true);
    }

    protected virtual void Despawn()
    {
        ObjPoolManager.instance.Despawn(transform);
        ScoreManager.instance.Kill();
        ScoreManager.instance.GoldAdd(1);
    }

    public virtual bool Heal()
    {
        int loseHp = this.hpMax - this.hp;
        int currentGold = ScoreManager.instance.GetGold();

        int cost = loseHp;

        if (currentGold <= loseHp) cost = currentGold;
        if (!ScoreManager.instance.GoldDeduct(cost)) return false;

        this.hp += cost;

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

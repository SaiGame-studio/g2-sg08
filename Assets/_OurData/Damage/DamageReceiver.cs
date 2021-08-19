using UnityEngine;

public class DamageReceiver : SaiBehaviour
{
    [Header("DamageReceiver")]
    [SerializeField] protected int hp = 1;
    [SerializeField] protected string deadEffect = "EnemyDeath1";

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
        bool isAlreadyPool = ObjPoolManager.instance.Pool().GetPrefabPool(transform) == null ? false : true;
        if (isAlreadyPool)
        {
            ObjPoolManager.instance.Despawn(transform);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log(transform.name + ": Not in Pool Delete");
        }
    }
}

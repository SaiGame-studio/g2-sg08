using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : SaiBehaviour
{
    [Header("DamageReceiver")]
    [SerializeField] protected int hp = 1;

    public virtual bool IsDead()
    {
        return this.hp <= 0;
    }

    public virtual void Receive(int damage)
    {
        this.hp -= damage;
    }
}

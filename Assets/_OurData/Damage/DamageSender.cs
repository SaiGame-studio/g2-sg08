using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : SaiBehaviour
{
    [Header("DamageSender")]
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.ColliderSendDamage(collision);
    }

    protected virtual void ColliderSendDamage(Collider2D collision)
    {
        DamageReceiver damageReveiver = collision.GetComponent<DamageReceiver>();
        if (damageReveiver == null) return;

        damageReveiver.Receive(this.damage);
    }
}

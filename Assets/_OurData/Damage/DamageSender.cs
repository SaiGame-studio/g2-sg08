using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : SaiBehaviour
{
    [Header("DamageSender")]
    public int damage = 1;


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("this: " + transform.name);
        Debug.Log("collision: " + collision.gameObject.name);
        this.ColliderSendDamage(collision);
    }

    protected virtual void ColliderSendDamage(Collision collision)
    {
        DamageReceiver damageReveiver = collision.gameObject.GetComponent<DamageReceiver>();
        if (damageReveiver == null) return;

        damageReveiver.Receive(this.damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : SaiBehaviour
{
    [Header("DamageSender")]
    [SerializeField] protected int damage = 1;

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("this: " + transform.name);
        //Debug.Log("collision: " + collision.gameObject.name);
        this.SendDamage(collision);
    }

    protected virtual void SendDamage(Collision collision)
    {
        DamageReceiver damageReveiver = collision.gameObject.GetComponent<DamageReceiver>();
        if (damageReveiver == null) return;

        damageReveiver.Receive(this.damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : SaiBehaviour
{
    [Header("DamageSender")]
    [SerializeField] protected int damage = 1;

    void OnCollisionEnter(Collision collision)
    {
        this.SendDamage(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        this.SendDamage(other.gameObject);
    }

    protected virtual void SendDamage(GameObject coliderObj)
    {
        DamageReceiver damageReveiver = coliderObj.GetComponent<DamageReceiver>();
        if (damageReveiver == null) return;

        damageReveiver.Receive(this.damage, this);
    }

    public virtual int SetDamage(int newDamage)
    {
        this.damage = newDamage;
        return this.damage;
    }
}

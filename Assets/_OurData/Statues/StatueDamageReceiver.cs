using UnityEngine;

public class StatueDamageReceiver : DamageReceiver
{
    [Header("Statue")]
    public StatueCtrl statueCtrl;

    protected override void LoadComponents()
    {
        this.LoadStatueCtrl();
    }

    protected virtual void LoadStatueCtrl()
    {
        if (this.statueCtrl != null) return;
        this.statueCtrl = GetComponent<StatueCtrl>();

        Debug.Log(transform.name + ": LoadStatueCtrl");
    }

    public override void Receive(int damage, DamageSender sender)
    {
        int senderLayer = sender.gameObject.layer;
        if (senderLayer != MyLayerManager.instance.layerEnemy) return;
        this.Receive(damage);
    }

    protected override void Despawn()
    {
        this.statueCtrl.statue.gameObject.SetActive(false);
        this.statueCtrl.gravestone.gameObject.SetActive(true);
        //this.statueCtrl._collider.enabled = false;
        gameObject.layer = MyLayerManager.instance.layerStatueBroken;
    }

    public override bool Heal()
    {
        bool healed = base.Heal();
        if (!healed) return false;

        this.statueCtrl.statue.gameObject.SetActive(true);
        this.statueCtrl.gravestone.gameObject.SetActive(false);
        //this.statueCtrl._collider.enabled = true;
        gameObject.layer = MyLayerManager.instance.layerStatue;

        return true;
    }
}

using TMPro;
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
        gameObject.layer = MyLayerManager.instance.layerStatueBroken;
    }

    public override bool Heal()
    {
        int cost = this.HealCost();
        Debug.Log(transform.name + ": Heal " + cost.ToString());

        if (!ScoreManager.instance.GoldDeduct(cost)) return false;

        this.hp += cost;

        this.statueCtrl.statue.gameObject.SetActive(true);
        this.statueCtrl.gravestone.gameObject.SetActive(false);
        gameObject.layer = MyLayerManager.instance.layerStatue;

        return true;
    }

    public virtual int HealCost()
    {
        int loseHp = this.hpMax - this.hp;
        int currentGold = ScoreManager.instance.GetGold();

        int cost = loseHp;

        if (currentGold <= loseHp) cost = currentGold;
        return cost;
    }
}

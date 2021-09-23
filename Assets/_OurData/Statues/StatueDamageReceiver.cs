

public class StatueDamageReceiver : DamageReceiver
{
    protected override void ResetValue()
    {
        this.hpMax = 20;
        this.hp = 20;
    }

    public override void Receive(int damage, DamageSender sender)
    {
        int senderLayer = sender.gameObject.layer;
        if (senderLayer != MyLayerManager.instance.layerEnemy) return;
        this.Receive(damage);
    }

    protected override void Despawn()
    {
        //is game over
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
}

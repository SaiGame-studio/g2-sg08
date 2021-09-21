public class StatueDamageReceiver : DamageReceiver
{
    public override void Receive(int damage, DamageSender sender)
    {
        int senderLayer = sender.gameObject.layer;
        if (senderLayer != MyLayerManager.instance.layerEnemy) return;
        this.Receive(damage);
    }
}

using UnityEngine;

public class BossDamageReceiver : EnemyDamageReceiver
{
    //[Header("Boss")]

    public override void Receive(int damage)
    {
        base.Receive(damage);

        int gold = Mathf.RoundToInt(damage / 3);
        if (gold < 1) gold = 1;
        TextManager.instance.TextGold(gold, transform.position);
        ScoreManager.instance.GoldAdd(gold);
    }
}

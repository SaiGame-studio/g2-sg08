using UnityEngine;

public class MyLevel : Level
{
    [Header("My Level")]
    [SerializeField] protected int levelCost = 10;

    public override int Up(int up)
    {
        if (!this.canLevelUp) return this.level;

        int cost = this.level * this.levelCost;
        if (!ScoreManager.instance.GoldDeduct(cost)) return this.level;

        this.level += up;
        return this.level;
    }
}

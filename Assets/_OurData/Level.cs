using UnityEngine;

public class Level : SaiBehaviour
{
    [Header("Level")]
    [SerializeField] protected bool canLevelUp = false;
    [SerializeField] protected int level;
    [SerializeField] protected int levelCost = 10;

    public virtual int Get()
    {
        return this.level;
    }

    public virtual int Up(int up)
    {
        if (!this.canLevelUp) return this.level;

        int cost = this.level * this.levelCost;
        if (!ScoreManager.instance.GoldDeduct(cost)) return this.level;

        this.level += up;
        return this.level;
    }
}

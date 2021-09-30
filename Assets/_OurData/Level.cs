using UnityEngine;

public class Level : SaiBehaviour
{
    [Header("Level")]
    [SerializeField] protected bool canLevelUp = false;
    [SerializeField] protected int level;

    public virtual int Get()
    {
        return this.level;
    }

    public virtual int Up(int up)
    {
        if (!this.canLevelUp) return this.level;

        this.level += up;
        return this.level;
    }

    public virtual int Set(int newLevel)
    {
        this.level = newLevel;
        return this.level;
    }
}

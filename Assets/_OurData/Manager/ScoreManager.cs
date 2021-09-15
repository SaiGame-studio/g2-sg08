using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Score")]
    [SerializeField] protected int gold = 0;
    [SerializeField] protected int kill = 0;

    private void Awake()
    {
        if (ScoreManager.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        ScoreManager.instance = this;
    }

    public virtual void Kill()
    {
        this.kill++;
    }

    public virtual void GoldAdd(int count)
    {
        this.gold += count;
    }

    public virtual void GoldDeduct(int count)
    {
        this.gold -= count;
    }
}

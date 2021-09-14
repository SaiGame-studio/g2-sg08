using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Layers")]
    [SerializeField] protected int gold = 0;
    [SerializeField] protected int killCount = 0;

    private void Awake()
    {
        if (ScoreManager.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        ScoreManager.instance = this;
    }

    public virtual void KillCount()
    {
        this.killCount++;
    }

    public virtual void GoldAdd(int count)
    {
        this.gold += count;
    }

}

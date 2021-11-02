using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;

    [Header("Game Level")]
    [SerializeField] protected int level = 0;
    [SerializeField] protected float secondPerLevel = 20;
    [SerializeField] protected float timer = 0;

    private void Awake()
    {
        if (GameLevelManager.instance != null) Debug.LogError("Only 1 GameLevelManager allow");
        GameLevelManager.instance = this;
    }

    private void FixedUpdate()
    {
        this.timer += Time.fixedDeltaTime;
        this.LevelCalculate();
    }

    protected void LevelCalculate()
    {
        this.level = (int) Mathf.Floor(this.timer / this.secondPerLevel);
        this.level += 1;
    }

    public virtual int Level()
    {
        return this.level;
    }

    public virtual float Timer()
    {
        return this.timer;
    }
}

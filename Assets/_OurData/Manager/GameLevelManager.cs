using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelManager : SaiBehaviour
{
    public static GameLevelManager instance;

    [Header("Statue")]
    public bool gameOver = false;
    public StatueCtrl statue1;
    public StatueCtrl statue2;
    public StatueCtrl statue3;
    public StatueCtrl statue4;
    public GameObject buttonGameOver;

    [Header("Game Level")]
    [SerializeField] protected int level = 0;
    [SerializeField] protected float secondPerLevel = 10;
    [SerializeField] protected float timer = 0;

    private void Awake()
    {
        if (GameLevelManager.instance != null) Debug.LogError("Only 1 GameLevelManager allow");
        GameLevelManager.instance = this;
    }

    private void Start()
    {
        this.buttonGameOver.SetActive(false);
    }

    private void FixedUpdate()
    {
        this.LevelCalculate();
        this.CheckGameOver();
    }

    protected virtual void CheckGameOver()
    {
        
        if (this.gameOver) return;
        if (!this.statue4.statueDamageReceiver.IsDead()) return;

        this.gameOver = true;
        this.buttonGameOver.SetActive(true);

        ScarecrowSpawner.instance.GameRenew();
        BossSpawner.instance.GameRenew();
        EnemySpawner.instance.GameRenew();

        SaveManager.instance.SaveGame();
    }

    public virtual void GameRenew()
    {
        ScoreManager.instance.GoldReset();
        ScoreManager.instance.KillReset();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        //this.level = 0;
        //this.timer = 0;
        //this.gameOver = false;
        //this.buttonGameOver.SetActive(false);

        //this.statue1.GameRenew();
        //this.statue2.GameRenew();
        //this.statue3.GameRenew();
        //this.statue4.GameRenew();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLastStatue();
    }

    protected virtual void LoadLastStatue()
    {
        if (this.statue4 != null) return;
        this.statue1 = GameObject.Find("TargetStatue1").GetComponent<StatueCtrl>();
        this.statue2 = GameObject.Find("TargetStatue2").GetComponent<StatueCtrl>();
        this.statue3 = GameObject.Find("TargetStatue3").GetComponent<StatueCtrl>();
        this.statue4 = GameObject.Find("TargetStatue4").GetComponent<StatueCtrl>();

        this.buttonGameOver = GameObject.Find("ButtonGameOver");

        Debug.Log(transform.name + ": LoadLastStatue");
    }

    protected void LevelCalculate()
    {
        if (this.gameOver) return;

        this.timer += Time.fixedDeltaTime;
        this.level = (int)Mathf.Floor(this.timer / this.secondPerLevel);
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

using TMPro;
using UnityEngine;

public class PlayerLevelUp : PlayerInteracByDistance
{
    [Header("Player Level Up")]
    [SerializeField] protected Transform chestOpened;
    [SerializeField] protected TextMeshPro textLevelCost;
    [SerializeField] protected int costBase = 70;
    [SerializeField] protected float costIncrease = 0.4f;
    [SerializeField] protected float costMulti = 7f;
    [SerializeField] protected int costCurrent = 0;
    [SerializeField] protected Vector3 spawnPos;


    private void FixedUpdate()
    {
        this.CheckCosting();
        this.CheckDistance();
        this.ChestOpening();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChestOpened();
        this.LoadTextLevelCost();
    }

    protected virtual void LoadChestOpened()
    {
        if (this.chestOpened != null) return;
        this.chestOpened = transform.Find("ChestOpened");
        Debug.Log(transform.name + ": LoadChestOpened");
    }

    protected virtual void LoadTextLevelCost()
    {
        if (this.textLevelCost != null) return;
        Transform hpObj = transform.Find("TextLevelCost");
        if (hpObj == null) return;
        if (hpObj) this.textLevelCost = hpObj.GetComponent<TextMeshPro>();
        Debug.Log(transform.name + ": LoadTextLevelCost");
    }

    protected virtual void ChestOpening()
    {
        bool chestOpened = this.chestOpened.gameObject.activeSelf;
        if (chestOpened == this.actived) return;

        this.chestOpened.gameObject.SetActive(this.actived);

        chestOpened = this.chestOpened.gameObject.activeSelf;
        this.LinkToInput(chestOpened);
    }

    protected virtual void CheckCosting()
    {
        HeroCtrl currentHero = PlayerManager.instance.currentHero;
        int currentLevel = currentHero.heroLevel.Get();
        float levelIncrease = currentLevel / 100;
        if (currentLevel > 5) levelIncrease *= this.costMulti;
        if (currentLevel > 10) levelIncrease *= this.costMulti;
        if (currentLevel > 15) levelIncrease *= this.costMulti;
        if (currentLevel > 20) levelIncrease *= this.costMulti;

        this.costCurrent = this.costBase * currentLevel;
        this.costCurrent += Mathf.RoundToInt(this.costCurrent * (this.costIncrease+ levelIncrease));

        this.textLevelCost.text = this.costCurrent.ToString("N0") + "G";
    }

    public override void Interact()
    {
        HeroCtrl currentHero = PlayerManager.instance.currentHero;
        HeroesManager heroesManager = currentHero.heroesManager;

        int currentLevel = currentHero.heroLevel.Get();
        if (!heroesManager.TryGetNextHero(currentLevel))
        {
            currentLevel = 0;
            Debug.LogWarning("Cant level up Hero");
            return;
        }

        if (!ScoreManager.instance.GoldDeduct(this.costCurrent)) return;

        HeroCtrl heroCtrl = heroesManager.GetNextHero(currentLevel);

        heroCtrl.transform.parent = PlayersHolder.instance.transform;

        PlayerManager.instance.playerMovement.inputHorizontalRaw = 1;
        this.spawnPos = currentHero.transform.position;
        spawnPos.y += 0.5f;

        heroCtrl.characterCtrl.enabled = false;
        heroCtrl.transform.position = spawnPos;
        heroCtrl.characterCtrl.enabled = true;

        PlayerManager.instance.SetPlayerCtrl(heroCtrl);
        ObjPoolManager.instance.Despawn(currentHero.transform);
    }
}

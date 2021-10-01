using UnityEngine;

public class PlayerLevelUp : PlayerInteractable
{
    [Header("Player Level Up")]
    [SerializeField] protected float distance = Mathf.Infinity;
    [SerializeField] protected float distanceLimit = 1.5f;
    [SerializeField] protected bool opened = false;
    [SerializeField] protected Transform chestOpened;
    [SerializeField] protected int cost = 100;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChestOpened();
    }

    protected virtual void LoadChestOpened()
    {
        if (this.chestOpened != null) return;
        this.chestOpened = transform.Find("ChestOpened");

        Debug.Log(transform.name + ": LoadChestOpened");
    }

    private void FixedUpdate()
    {
        this.CheckDistance();
        this.ChestOpening();
    }

    protected virtual void CheckDistance()
    {
        HeroCtrl hero = PlayerManager.instance.currentHero;
        this.distance = Vector3.Distance(transform.position, hero.transform.position);

        this.opened = false;
        if (this.distance > this.distanceLimit) return;

        this.opened = true;
    }

    protected virtual void ChestOpening()
    {
        bool chestOpened = this.chestOpened.gameObject.activeSelf;
        if (chestOpened == this.opened) return;
        this.chestOpened.gameObject.SetActive(this.opened);

        chestOpened = this.chestOpened.gameObject.activeSelf;
        this.LinkToInput(chestOpened);
    }

    public override void Interact()
    {
        HeroCtrl currentHero = PlayerManager.instance.currentHero;
        HeroesManager heroesManager = currentHero.heroManagers;

        int currentLevel = currentHero.heroLevel.Get();
        if (!heroesManager.TryGetNextHero(currentLevel))
        {
            Debug.LogWarning("Cant level up Hero");
            return;
        }

        int levelCost = this.cost * currentLevel;
        if (!ScoreManager.instance.GoldDeduct(levelCost)) return;

        HeroCtrl heroObj = heroesManager.GetNextHero(currentLevel);

        heroObj.transform.parent = PlayersHolder.instance.transform;
        heroObj.transform.position = currentHero.transform.position;

        PlayerManager.instance.SetPlayerCtrl(heroObj);
    }
}

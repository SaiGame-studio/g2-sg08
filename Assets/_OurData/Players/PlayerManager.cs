using Assets.HeroEditor.Common.ExampleScripts;
using UnityEngine;

public class PlayerManager : SaiBehaviour
{
    public static PlayerManager instance;

    public HeroCtrl currentHero;
    public PlayerInput playerInput;
    public PlayerAttacking playerAttacking;
    public PlayerMovement playerMovement;
    public BowExample bowExample;

    private void Awake()
    {
        if (PlayerManager.instance != null) Debug.LogError("Only 1 PlayerManager allow");
        PlayerManager.instance = this;
    }

    private void Start()
    {
        this.LoadPlayers();
    }

    protected override void LoadComponents()
    {
        this.LoadPlayerComponents();
    }

    protected virtual void LoadPlayerComponents()
    {
        if (this.playerAttacking != null) return;
        this.playerAttacking = transform.GetComponentInChildren<PlayerAttacking>();
        this.playerMovement = transform.GetComponentInChildren<PlayerMovement>();
        this.bowExample = transform.GetComponentInChildren<BowExample>();
        this.playerInput = transform.GetComponentInChildren<PlayerInput>();

        Debug.Log(transform.name + ": LoadPlayerComponents");
    }

    protected virtual void LoadPlayers()
    {
        HeroCtrl heroCtrl;
        Vector3 vector3 = transform.position;
        vector3.x -= 7;
        foreach (HeroesManager heroesManager in HeroManagers.instance.heroManagers)
        {
            vector3.x += 3;
            heroCtrl = heroesManager.GetHero();
            heroCtrl.transform.position = vector3;
            heroCtrl.transform.parent = PlayersHolder.instance.transform;

            PlayersHolder.instance.heroCtrls.Add(heroCtrl);

            this.SetPlayerCtrl(heroCtrl);
        }
    }

    public virtual void SetPlayerCtrl(HeroCtrl heroCtrl)
    {
        this.currentHero = heroCtrl;

        this.playerAttacking.character = this.currentHero.character;
        this.playerAttacking.firearm = this.currentHero.firearm;
        this.playerAttacking.armL = this.currentHero.armL;
        this.playerAttacking.armR = this.currentHero.armR;

        this.playerMovement.character = this.currentHero.character;
        this.playerMovement.charCtrl = this.currentHero.characterCtrl;
        this.playerMovement.ResetMyGround();
    }

    public virtual bool ChoosePlayer(string chooseClass)
    {
        Debug.Log("Choose:" + chooseClass);

        string profileClass;
        foreach (HeroCtrl heroCtrl in PlayersHolder.instance.heroCtrls)
        {
            profileClass = heroCtrl.heroProfile.HeroClass();
            if (profileClass == chooseClass)
            {
                this.SetPlayerCtrl(heroCtrl);
                return true;
            }
        }

        Debug.LogError("Class you choose not exist:" + chooseClass);
        return false;
    }
}

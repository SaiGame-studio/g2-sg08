using Assets.HeroEditor.Common.ExampleScripts;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SaiBehaviour
{
    [Header("Manager")]
    public static PlayerManager instance;
    public HeroCtrl currentHero;
    public PlayerInput playerInput;
    public PlayerAttacking playerAttacking;
    public PlayerMovement playerMovement;
    public BowExample bowExample;
    [SerializeField] protected string firstClass = "Shooter";
    [SerializeField] protected int playerIndex = 0;

    private void Awake()
    {
        if (PlayerManager.instance != null) Debug.LogError("Only 1 PlayerManager allow");
        PlayerManager.instance = this;
    }

    private void Start()
    {
        this.LoadFirstPlayer();
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

    public virtual void LoadRandomPlayer()
    {
        HeroCtrl heroCtrl;
        Vector3 vector3 = this.currentHero.transform.position;
        vector3.x += 3;

        heroCtrl = HeroManagers.instance.RandomHero();
        heroCtrl.transform.parent = PlayersHolder.instance.transform;

        heroCtrl.characterCtrl.enabled = false;
        heroCtrl.transform.position = vector3;
        heroCtrl.characterCtrl.enabled = true;

        heroCtrl.gameObject.SetActive(true);
        PlayersHolder.instance.heroCtrls.Add(heroCtrl);

        int newIndex = PlayersHolder.instance.heroCtrls.Count;
        this.ChoosePlayer(newIndex);
    }

    protected virtual void LoadFirstPlayer()
    {
        HeroCtrl heroCtrl;
        Vector3 vector3 = transform.position;
        foreach (HeroesManager heroesManager in HeroManagers.instance.heroManagers)
        {
            string className = heroesManager.heroProfile.HeroClass();
            if (className != this.firstClass) continue;

            heroCtrl = heroesManager.GetHero();
            heroCtrl.transform.position = vector3;
            heroCtrl.transform.parent = PlayersHolder.instance.transform;

            this.SetPlayerCtrl(heroCtrl);
            PlayersHolder.instance.heroCtrls.Add(heroCtrl);
        }
    }

    public virtual void SetPlayerCtrl(HeroCtrl heroCtrl)
    {
        HeroCtrl lastHero = this.currentHero;
        if(lastHero) lastHero.playerAutoAttack.gameObject.SetActive(true);

        this.currentHero = heroCtrl;
        this.currentHero.playerAutoAttack.gameObject.SetActive(false);

        this.playerAttacking.character = this.currentHero.character;
        this.playerAttacking.firearm = this.currentHero.firearm;
        this.playerAttacking.armL = this.currentHero.armL;
        this.playerAttacking.armR = this.currentHero.armR;

        this.playerMovement.character = this.currentHero.character;
        this.playerMovement.charCtrl = this.currentHero.characterCtrl;

        this.currentHero.gameObject.SetActive(true);//TODO: need for unknow bug fix
        this.playerMovement.ResetMyGround();        
    }

    public virtual void ChoosePlayer(int playerIndex)
    {
        this.playerIndex = playerIndex;

        playerIndex -= 1;
        List<HeroCtrl> heroCtrls = PlayersHolder.instance.heroCtrls;

        if (playerIndex >= heroCtrls.Count) return;

        HeroCtrl heroCtrl = PlayersHolder.instance.heroCtrls[playerIndex];
        this.SetPlayerCtrl(heroCtrl);
    }
}

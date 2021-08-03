using Assets.HeroEditor.Common.EditorScripts;
using Assets.HeroEditor.Common.ExampleScripts;
using UnityEngine;

public class PlayerManager : SaiBehaviour
{
    public HeroCtrl currentHero;
    public PlayerAttacking playerAttacking;
    public PlayerMovement playerMovement;
    public BowExample bowExample;

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

        Debug.Log(transform.name + ": LoadPlayerComponents");
    }
    
    protected virtual void LoadPlayers()
    {
        GameObject hero;
        Vector3 vector3 = transform.position;
        vector3.x -= 7;
        foreach (HeroesManager heroesManager in HeroManagers.instance.heroManagers)
        {
            vector3.x += 3;
            hero = heroesManager.GetHero();
            hero.transform.position = vector3;
            hero.transform.parent = PlayersHolder.instance.transform;

            HeroCtrl heroCtrl = hero.GetComponent<HeroCtrl>();
            PlayersHolder.instance.heroCtrls.Add(heroCtrl);

            this.SetPlayerCtrl(hero);
        }
    }

    public virtual void SetPlayerCtrl(GameObject obj)
    {
        this.currentHero = obj.GetComponent<HeroCtrl>();

        this.playerAttacking.character = this.currentHero.character;
        this.playerAttacking.firearm = this.currentHero.firearm;
        this.playerAttacking.armL = this.currentHero.armL;
        this.playerAttacking.armR = this.currentHero.armR;

        this.playerMovement.character = this.currentHero.character;
        this.playerMovement.charCtrl = this.currentHero.characterCtrl;
    }
}

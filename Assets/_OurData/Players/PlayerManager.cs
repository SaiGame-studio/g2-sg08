using Assets.HeroEditor.Common.EditorScripts;
using Assets.HeroEditor.Common.ExampleScripts;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public HeroCtrl currentHero;
    public PlayerAttacking playerAttacking;
    public PlayerMovement playerMovement;
    public BowExample bowExample;
    public AnimationManager animationManager;
    public HeroesManager[] heroManagers;
    public HeroesManager heroesManager;

    private void Start()
    {
        //this.LoadPlayer();
        this.LoadPlayers();
    }

    private void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadPlayerComponents();
        this.LoadHeroComponents();
    }

    protected virtual void LoadPlayerComponents()
    {
        if (this.playerAttacking != null) return;
        this.playerAttacking = transform.GetComponentInChildren<PlayerAttacking>();
        this.playerMovement = transform.GetComponentInChildren<PlayerMovement>();
        this.bowExample = transform.GetComponentInChildren<BowExample>();
        this.animationManager = transform.GetComponentInChildren<AnimationManager>();

        Debug.Log(transform.name + ": LoadPlayerComponents");
    }

    protected virtual void LoadHeroComponents()
    {
        //TODO: this is not done
        this.heroesManager = GameObject.Find("ShooterManager").GetComponent<HeroesManager>();

        if (this.heroManagers.Length > 0) return;
        GameObject obj = GameObject.Find("HeroManagers");
        this.heroManagers = obj.GetComponentsInChildren<HeroesManager>();
        Debug.Log(transform.name + ": LoadHeroComponents");
    }

    protected virtual void LoadPlayer()
    {
        GameObject obj = this.heroesManager.GetHero();
        this.SetPlayerCtrl(obj);
    }

    protected virtual void LoadPlayers()
    {
        GameObject hero;
        Vector3 vector3 = transform.position;
        vector3.x -= 7;
        foreach (HeroesManager heroesManager in this.heroManagers)
        {
            vector3.x += 3;
            hero = heroesManager.GetHero();
            hero.transform.position = vector3;
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

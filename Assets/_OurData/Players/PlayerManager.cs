using Assets.HeroEditor.Common.EditorScripts;
using Assets.HeroEditor.Common.ExampleScripts;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerAttacking playerAttacking;
    public PlayerMovement playerMovement;
    public BowExample bowExample;
    public AnimationManager animationManager;
    public HeroesManager heroesManager;
    public HeroCtrl currentHero;

    private void Start()
    {
        this.LoadPlayer();
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
        this.heroesManager = GameObject.Find("WarriorManager").GetComponent<HeroesManager>();
    }

    protected virtual void LoadPlayer()
    {
        GameObject obj = this.heroesManager.GetHero();
        this.currentHero = obj.GetComponent<HeroCtrl>();
    }
}

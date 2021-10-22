using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManagers : MonoBehaviour
{
    public static HeroManagers instance;
    public HeroesManager[] heroManagers;

    private void Awake()
    {
        if (HeroManagers.instance != null) Debug.LogError("Only 1 HeroManagers allow");
        HeroManagers.instance = this;
    }

    private void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadHeroComponents();
    }

    protected virtual void LoadHeroComponents()
    {
        if (this.heroManagers.Length > 0) return;
        this.heroManagers = transform.GetComponentsInChildren<HeroesManager>();
        Debug.Log(transform.name + ": LoadHeroComponents");
    }

    public virtual HeroCtrl RandomHero()
    {
        int maxRan = this.heroManagers.Length;
        //int ran1 = Random.RandomRange(0, maxRan);
        int ran = Random.Range(0, maxRan);
        HeroesManager heroesManager = this.heroManagers[ran];
        HeroCtrl heroCtrl = heroesManager.GetHero();
        return heroCtrl;
    }
}

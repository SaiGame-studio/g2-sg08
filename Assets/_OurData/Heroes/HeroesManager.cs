using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    [Header("Hero")]
    public HeroProfile heroProfile;
    public List<HeroCtrl> heroes = new List<HeroCtrl>();

    private void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadHeros();
        this.LoadHeroProfile();
    }

    protected virtual void LoadHeros()
    {
        if (this.heroes.Count > 0) return;
        foreach (HeroCtrl heroCtrl in transform.GetComponentsInChildren<HeroCtrl>())
        {
            this.heroes.Add(heroCtrl);
        }

        Debug.Log(transform.name + ": LoadHeros");
    }

    protected virtual void LoadHeroProfile()
    {
        if (this.heroProfile != null) return;
        this.heroProfile = GetComponent<HeroProfile>();
        Debug.Log(transform.name + ": LoadHeroProfile");
    }

    public virtual HeroCtrl GetHero()
    {
        return this.GetHero(0);
    }

    public virtual HeroCtrl GetHero(int index)
    {
        if (index >= this.heroes.Count) return null;

        GameObject heroObj = this.heroes[index].gameObject;
        GameObject hero = Instantiate(heroObj);
        HeroCtrl heroCtrl = hero.GetComponent<HeroCtrl>();
        heroCtrl.heroesManager = this;
        return heroCtrl;
    }

    public virtual HeroCtrl GetNextHero(int currentLevel)
    {
        return this.GetHero(currentLevel);
    }

    public virtual bool TryGetNextHero(int index)
    {
        if (index >= this.heroes.Count) return false;
        return true;
    }
}

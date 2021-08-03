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

    public virtual GameObject GetHero()
    {
        GameObject heroObj = this.heroes[0].gameObject;
        GameObject hero = Instantiate(heroObj, new Vector3(0, 0, 0), transform.rotation);
        return hero;
    }
}

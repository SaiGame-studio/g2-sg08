using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    [Header("Hero")]
    public List<HeroCtrl> heroes = new List<HeroCtrl>();

    private void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadHeros();
    }

    protected virtual void LoadHeros()
    {
        if (this.heroes.Count > 0) return;
        foreach(HeroCtrl heroCtrl in transform.GetComponentsInChildren<HeroCtrl>())
        {
            this.heroes.Add(heroCtrl);
        }

        Debug.Log(transform.name+ ": LoadHeros");
    }

    public virtual GameObject GetHero()
    {
        GameObject heroObj = this.heroes[0].gameObject;
        GameObject hero = Instantiate(heroObj, new Vector3(0, 0, 0), transform.rotation);
        return hero;
    }
}

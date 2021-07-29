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
}

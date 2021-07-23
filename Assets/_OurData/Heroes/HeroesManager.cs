using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    [Header("Hero")]
    public List<HeroCtrl> herros = new List<HeroCtrl>();

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
        if (this.herros.Count > 0) return;
        foreach(HeroCtrl heroCtrl in transform.GetComponentsInChildren<HeroCtrl>())
        {
            this.herros.Add(heroCtrl);
        }

        Debug.Log(transform.name+ ": LoadHeros");
    }
}

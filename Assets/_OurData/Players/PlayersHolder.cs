using System.Collections.Generic;
using UnityEngine;

public class PlayersHolder : MonoBehaviour
{
    public static PlayersHolder instance;
    public List<HeroCtrl> heroCtrls;

    private void Awake()
    {
        if (PlayersHolder.instance != null) Debug.LogError("Only 1 PlayersHolder allow");
        PlayersHolder.instance = this;
    }

    private void FixedUpdate()
    {
        this.LoadHeroes();
    }

    protected virtual void LoadHeroes()
    {
        this.RemoveInactive();
        this.AddActive();
        this.ShowTextIndex();
    }

    protected virtual void ShowTextIndex()
    {
        HeroCtrl heroCtrl;

        if (this.heroCtrls.Count == 1)
        {
            heroCtrl = this.heroCtrls[0];
            heroCtrl.textPlayerIndex.text = "";
            return;
        }

        for (int i = 1; i <= this.heroCtrls.Count; i++)
        {
            heroCtrl = this.heroCtrls[i - 1];
            heroCtrl.textPlayerIndex.text = i.ToString();
            heroCtrl.textPlayerIndex.color = Color.white;

            if (heroCtrl == PlayerManager.instance.currentHero)
            {
                heroCtrl.textPlayerIndex.color = Color.green;
            }
        }
    }

    protected virtual void RemoveInactive()
    {
        HeroCtrl heroCtrl;
        for (int i = 0; i < this.heroCtrls.Count; i++)
        {
            heroCtrl = this.heroCtrls[i];
            if (heroCtrl.gameObject.activeSelf) continue;
            this.heroCtrls.Remove(heroCtrl);
        }
    }

    protected virtual void AddActive()
    {
        HeroCtrl heroCtrl;
        foreach (Transform obj in transform)
        {
            if (!obj.gameObject.activeSelf) continue;
            heroCtrl = obj.GetComponent<HeroCtrl>();

            if (this.heroCtrls.Contains(heroCtrl)) continue;

            this.heroCtrls.Add(heroCtrl);
        }
    }

    public virtual HeroCtrl GetHero(string name)
    {
        return this.heroCtrls[0];
    }
}

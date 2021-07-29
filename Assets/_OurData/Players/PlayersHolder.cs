using System.Collections;
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

    public virtual HeroCtrl GetHero(string name)
    {
        //TODO: get by name
        return this.heroCtrls[0];
    }
}

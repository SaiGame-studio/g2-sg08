using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHolder : MonoBehaviour
{
    public List<HeroCtrl> heroCtrls;

    public virtual HeroCtrl GetHero(string name)
    {
        //TODO: get by name
        return this.heroCtrls[0];
    }
}

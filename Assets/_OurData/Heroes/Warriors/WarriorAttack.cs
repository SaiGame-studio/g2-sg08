using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using UnityEngine;

public class WarriorAttack : SaiBehaviour
{
    //[Header("Warrior Attack")]
    //public HeroesManager heroesManager;

    public virtual void Attack()
    {
        Debug.Log(transform.name + ": WarriorAttack");
    }

}

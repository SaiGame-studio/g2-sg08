using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using UnityEngine;

public class WarriorCtrl : HeroCtrl
{
    [Header("Warrior")]
    public WarriorAttack warriorAttack;

    private void OnEnable()
    {
        this.FixCharacter();
        this.EventRegistry();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWarriorAttack();
    }

    protected virtual void LoadWarriorAttack()
    {
        if (this.warriorAttack != null) return;
        this.warriorAttack = GetComponent<WarriorAttack>();
        Debug.Log(transform.name + ": LoadWarriorAttack");
    }

    protected virtual void EventRegistry()
    {
        this.character.Animator.GetComponent<AnimationEvents>().OnCustomEvent += OnAnimationEvent;
    }

    protected virtual void EventUnRegistry()
    {
        this.character.Animator.GetComponent<AnimationEvents>().OnCustomEvent -= OnAnimationEvent;
    }

    protected void OnAnimationEvent(string eventName)
    {
        Debug.Log(transform.name + " OnAnimationEvent " + eventName);
        this.warriorAttack.Attack();
    }
}

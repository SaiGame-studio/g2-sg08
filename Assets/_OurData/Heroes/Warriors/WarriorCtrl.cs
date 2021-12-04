using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

public class WarriorCtrl : HeroCtrl
{
    [Header("Warrior")]
    public WarriorAttack warriorAttack;
    public Transform strikePoint;

    private void OnEnable()
    {
        this.FixCharacter();
        this.EventRegistry();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWarriorAttack();
        this.LoadStrikePoint();
    }

    protected virtual void LoadStrikePoint()
    {
        if (this.strikePoint != null) return;
        this.strikePoint = transform.Find("StrikePoint");
        Debug.Log(transform.name + ": LoadStrikePoint");
    }

    protected virtual void LoadWarriorAttack()
    {
        if (this.warriorAttack != null) return;
        this.warriorAttack = GetComponent<WarriorAttack>();
        Debug.Log(transform.name + ": LoadWarriorAttack");
    }

    protected virtual void EventRegistry()
    {
        this.character.Animator.GetComponent<AnimationEvents>().OnCustomEvent += this.OnAnimationEvent;
    }

    protected virtual void EventUnRegistry()
    {
        this.character.Animator.GetComponent<AnimationEvents>().OnCustomEvent -= this.OnAnimationEvent;
    }

    protected void OnAnimationEvent(string eventName)
    {
        if (eventName == "Hit") this.warriorAttack.Attack();
        else Debug.Log(transform.name + " eventName " + eventName);
    }

    protected override void FixCharacter()
    {
        base.FixCharacter();
        this.strikePoint.parent = this.character.transform;
    }

    public override void AutoAttack()
    {
        this.character.Slash();
        //Debug.Log(transform.name + " AutoAttack");
    }
}

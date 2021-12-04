using UnityEngine;

public class SkillCtrl : SaiBehaviour
{
    public DamageSender damageSender;
    public Transform skill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDamageSender();
        this.LoadSkill();
    }

    protected virtual void LoadSkill()
    {
        if (this.skill != null) return;
        this.skill = transform.Find("Skill");
        Debug.Log(transform.name + ": LoadSkill");
    }

    protected virtual void LoadDamageSender()
    {
        if (this.damageSender != null) return;
        this.damageSender = GetComponent<DamageSender>();
        Debug.Log(transform.name + ": LoadDamageSender");
    }
}

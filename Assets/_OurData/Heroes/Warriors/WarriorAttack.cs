using UnityEngine;

public class WarriorAttack : SaiBehaviour
{
    [Header("Warrior Attack")]
    public WarriorCtrl warriorCtrl;
    [SerializeField] protected string skillName = "SkillSword";

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWarriorCtrl();
    }

    protected virtual void LoadWarriorCtrl()
    {
        if (this.warriorCtrl != null) return;
        this.warriorCtrl = GetComponent<WarriorCtrl>();
        Debug.Log(transform.name + ": LoadWarriorCtrl");
    }

    public virtual void Attack()
    {
        //Debug.Log(transform.name + ": WarriorAttack");
        Transform skillObj = ObjPoolManager.instance.Spawn(this.skillName);
        skillObj.transform.position = this.warriorCtrl.strikePoint.position;
        skillObj.transform.parent = transform;

        DamageSender damageSender = skillObj.GetComponent<DamageSender>();
        HeroLevel heroLevel = this.warriorCtrl.heroLevel;
        damageSender.SetDamage(heroLevel.Get());

        Vector3 direction = this.warriorCtrl.GetTargetDirection();
        SwordSkillCtrl swordSkillCtrl = skillObj.GetComponent<SwordSkillCtrl>();
        swordSkillCtrl.Turn(direction);

        skillObj.gameObject.SetActive(true);

    }
}

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
        Debug.Log(transform.name + ": WarriorAttack");
        Transform skillObj = ObjPoolManager.instance.Spawn(this.skillName);
        skillObj.transform.position = this.warriorCtrl.strikePoint.position;
        skillObj.transform.parent = transform;
        skillObj.gameObject.SetActive(true);

        DamageSender damageSender = skillObj.GetComponent<DamageSender>();
        HeroLevel heroLevel = this.warriorCtrl.heroLevel;
        damageSender.SetDamage(heroLevel.Get());
    }
}

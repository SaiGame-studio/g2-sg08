using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using UnityEngine;

public class HeroEditorFix : SaiBehaviour
{
    public static HeroEditorFix instance;

    private void Awake()
    {
        if (HeroEditorFix.instance != null) Debug.LogError("Only 1 HeroEditorFix allow");
        HeroEditorFix.instance = this;
    }

    public virtual Bullet CreateBullet(FirearmFire firearmFire, Firearm Firearm)
    {
        Transform tBullet = ObjPoolManager.instance.Spawn("Bullet", Firearm.FireTransform);
        Bullet bullet = tBullet.GetComponent<Bullet>();

        HeroCtrl heroCtrl = this.GetHeroCtrl(firearmFire);
        int level = heroCtrl.heroLevel.Get();

        DamageSender damageSender = tBullet.GetComponent<DamageSender>();
        damageSender.SetDamage(level);

        tBullet.gameObject.SetActive(true);

        return bullet;
    }

    protected virtual HeroCtrl GetHeroCtrl(FirearmFire firearmFire)
    {
        Transform hero = firearmFire.transform.parent.parent.parent.parent.parent.parent.parent.parent;
        HeroCtrl heroCtrl = hero.GetComponent<HeroCtrl>();
        return heroCtrl;
    }
}








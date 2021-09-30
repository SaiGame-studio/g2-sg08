using UnityEngine;

public class HeroLevel : Level
{
    [Header("Hero")]
    public HeroCtrl heroCtrl;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHeroCtrl();
        this.LoadGetLevel();
    }

    protected virtual void LoadHeroCtrl()
    {
        if (this.heroCtrl != null) return;
        this.heroCtrl = GetComponent<HeroCtrl>();

        Debug.Log(transform.name + ": LoadHeroCtrl");
    }

    protected virtual void LoadGetLevel()
    {
        if (this.level != 0) return;
        string name = gameObject.name;
        string className = this.heroCtrl.heroProfile.HeroClass();
        name = name.Replace(className, "");
        int level = int.Parse(name);
        this.Set(level);

        Debug.Log(transform.name + ": LoadGetLevel");
    }

}

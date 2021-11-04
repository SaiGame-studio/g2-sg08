using UnityEngine;

public class StatueLevel : MyLevel
{
    [Header("Statue")]
    public StatueCtrl statueCtrl;

    protected override void LoadComponents()
    {
        this.LoadStatueCtrl();
    }

    protected virtual void LoadStatueCtrl()
    {
        if (this.statueCtrl != null) return;
        this.statueCtrl = GetComponent<StatueCtrl>();

        Debug.Log(transform.name + ": LoadStatueCtrl");
    }

    public override int Up(int up)
    {
        base.Up(up);

        Debug.Log(transform.name + ": Up " + up.ToString());

        int newHpMax = this.LevelCost(this.level);
        this.statueCtrl.statueDamageReceiver.SetHPMax(newHpMax);
        this.statueCtrl.statueDamageReceiver.SetHP(newHpMax);

        return this.level;
    }

    public virtual int LevelCost(int level)
    {
        int newHpMax = level * this.levelCost;
        return newHpMax;
    }

    public virtual void GameRenew()
    {
        this.level = 0;
    }
}

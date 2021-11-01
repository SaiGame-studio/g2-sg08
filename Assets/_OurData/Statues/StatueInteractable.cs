using TMPro;
using UnityEngine;

public class StatueInteractable : PlayerInteracByDistance
{
    [Header("Statue")]
    public StatueCtrl statueCtrl;
    [SerializeField] protected TextMeshPro textLevelCost;

    private void FixedUpdate()
    {
        this.ShowLevelCost();
        this.CheckDistance();
        this.CheckActivate();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.distanceLimit = 2f;
    }

    protected override void LoadComponents()
    {
        this.LoadStatueCtrl();
        this.LoadTextLevelCost();
    }

    protected virtual void LoadTextLevelCost()
    {
        if (this.textLevelCost != null) return;
        Transform hpObj = transform.Find("TextLevelCost");
        if (hpObj == null) return;
        if (hpObj) this.textLevelCost = hpObj.GetComponent<TextMeshPro>();
        Debug.Log(transform.name + ": LoadTextHP");
    }

    protected virtual void ShowLevelCost()
    {
        if (this.textLevelCost == null) return;

        int cost;
        int level = this.statueCtrl.statueLevel.Get() + 1;

        if (this.statueCtrl.statueDamageReceiver.IsHPFull()) cost = this.statueCtrl.statueLevel.LevelCost(level);
        else cost = this.statueCtrl.statueDamageReceiver.HealCost();

        this.textLevelCost.text = "-" + cost.ToString() + "G";
    }

    protected virtual void LoadStatueCtrl()
    {
        if (this.statueCtrl != null) return;
        this.statueCtrl = GetComponent<StatueCtrl>();

        Debug.Log(transform.name + ": LoadStatueCtrl");
    }

    protected virtual void CheckActivate()
    {
        GameObject statuActive = this.statueCtrl.statueActive.gameObject;
        bool chestOpened = statuActive.activeSelf;
        if (chestOpened == this.actived) return;

        statuActive.SetActive(this.actived);

        chestOpened = statuActive.activeSelf;
        this.LinkToInput(chestOpened);
    }

    public override void Interact()
    {
        if (this.statueCtrl.statueDamageReceiver.IsHPFull()) this.statueCtrl.statueLevel.Up(1);
        else this.statueCtrl.statueDamageReceiver.Heal();
    }
}

using UnityEngine;

public class StatueCtrl : SaiBehaviour
{
    [Header("Statue")]
    public StatueLevel statueLevel;
    public StatueInteractable statueInteractable;
    public StatueDamageReceiver statueDamageReceiver;

    protected override void LoadComponents()
    {
        this.LoadStatue();
        this.LoadStatueInteractable();
        this.LoadStatueDamageReceiver();
    }

    protected virtual void LoadStatue()
    {
        if (this.statueLevel != null) return;
        this.statueLevel = GetComponent<StatueLevel>();

        Debug.Log(transform.name + ": LoadStatue");
    }

    protected virtual void LoadStatueInteractable()
    {
        if (this.statueInteractable != null) return;
        this.statueInteractable = GetComponent<StatueInteractable>();

        Debug.Log(transform.name + ": LoadStatueInteractable");
    }

    protected virtual void LoadStatueDamageReceiver()
    {
        if (this.statueDamageReceiver != null) return;
        this.statueDamageReceiver = GetComponent<StatueDamageReceiver>();

        Debug.Log(transform.name + ": LoadStatueDamageReceiver");
    }
}

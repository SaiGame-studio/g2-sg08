using UnityEngine;

public class StatueCtrl : SaiBehaviour
{
    [Header("Statue")]
    public StatueLevel statueLevel;
    public StatueInteractable statueInteractable;

    protected override void LoadComponents()
    {
        this.LoadStatue();
        this.LoadStatueInteractable();
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
}

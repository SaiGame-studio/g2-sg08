using UnityEngine;

public class StatueCtrl : SaiBehaviour
{
    [Header("Statue")]
    public StatueLevel statueLevel;
    public StatueInteractable statueInteractable;
    public StatueDamageReceiver statueDamageReceiver;
    public Collider _collider;
    public Transform statue;
    public Transform statueActive;
    public Transform gravestone;

    protected override void LoadComponents()
    {
        this.LoadStatue();
        this.LoadStatueInteractable();
        this.LoadStatueDamageReceiver();
        this.LoadColider();
    }

    protected virtual void LoadStatue()
    {
        if (this.statueLevel != null) return;
        this.statueLevel = GetComponent<StatueLevel>();
        this.statue = transform.Find("Statue");

        this.statueActive = this.statue.Find("Active");
        this.statueActive.gameObject.SetActive(false);

        this.gravestone = transform.Find("Gravestone");


        this.gravestone.gameObject.SetActive(false);

        Debug.Log(transform.name + ": LoadStatue");
    }

    protected virtual void LoadColider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<Collider>();
        this._collider.isTrigger = true;

        Debug.Log(transform.name + ": LoadColider");
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

using UnityEngine;

public class PlayerLevelUp : SaiBehaviour
{
    [Header("Player Level Up")]
    [SerializeField] protected float distance = Mathf.Infinity;
    [SerializeField] protected float distanceLimit = 1.5f;
    [SerializeField] protected bool opened = false;
    [SerializeField] protected Transform chestOpened;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChestOpened();
    }

    protected virtual void LoadChestOpened()
    {
        if (this.chestOpened != null) return;
        this.chestOpened = transform.Find("ChestOpened");

        Debug.Log(transform.name + ": LoadChestOpened");
    }

    private void FixedUpdate()
    {
        this.CheckDistance();
        this.ChestOpening();
    }

    protected virtual void CheckDistance()
    {
        HeroCtrl hero = PlayerManager.instance.currentHero;
        this.distance = Vector3.Distance(transform.position, hero.transform.position);


        this.opened = false;
        if (this.distance > this.distanceLimit) return;

        this.opened = true;
    }

    protected virtual void ChestOpening()
    {
        bool status = this.chestOpened.gameObject.activeSelf;
        if (status == this.opened) return;
        this.chestOpened.gameObject.SetActive(this.opened);
    }
}

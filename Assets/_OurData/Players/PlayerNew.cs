using UnityEngine;

public class PlayerNew : PlayerInteracByDistance
{
    [Header("Player New")]
    [SerializeField] protected Transform fire;
    [SerializeField] protected int cost = 50;
    [SerializeField] protected Vector3 spawnPos;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFire();
    }

    protected virtual void LoadFire()
    {
        if (this.fire != null) return;
        this.fire = transform.Find("Campfire").Find("Fire");

        Debug.Log(transform.name + ": LoadFire");
    }

    private void FixedUpdate()
    {
        this.CheckDistance();
        this.ChestOpening();
    }

    protected virtual void ChestOpening()
    {
        bool objectActive = this.fire.gameObject.activeSelf;
        if (objectActive == this.actived) return;

        this.fire.gameObject.SetActive(this.actived);

        objectActive = this.fire.gameObject.activeSelf;
        this.LinkToInput(objectActive);
    }

    public override void Interact()
    {
        Debug.Log("Interact");
    }
}

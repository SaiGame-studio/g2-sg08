using TMPro;
using UnityEngine;

public class PlayerNew : PlayerInteracByDistance
{
    [Header("Player New")]
    [SerializeField] protected Transform fire;
    [SerializeField] protected TextMeshPro textPlayerCost;
    [SerializeField] protected int costBase = 100;
    [SerializeField] protected int costCurrent = 100;
    [SerializeField] protected int costMulti = 2;
    [SerializeField] protected Vector3 spawnPos;
    [SerializeField] protected int playerMax = 7;

    private void FixedUpdate()
    {
        this.CheckCosting();
        this.CheckDistance();
        this.ChestOpening();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFire();
        this.LoadTextPlayerCost();
    }

    protected virtual void LoadTextPlayerCost()
    {
        if (this.textPlayerCost != null) return;
        Transform hpObj = transform.Find("TextPlayerCost");
        if (hpObj == null) return;
        if (hpObj) this.textPlayerCost = hpObj.GetComponent<TextMeshPro>();
        Debug.Log(transform.name + ": LoadTextLevelCost");
    }

    protected virtual void CheckCosting()
    {
        int currentLevel = PlayersHolder.instance.heroCtrls.Count;
        this.costCurrent = this.costBase * (currentLevel * this.costMulti);

        this.textPlayerCost.text = this.costCurrent.ToString() + "G";
    }

    protected virtual void LoadFire()
    {
        if (this.fire != null) return;
        this.fire = transform.Find("Campfire").Find("Fire");

        Debug.Log(transform.name + ": LoadFire");
    }

    protected virtual void ChestOpening()
    {
        if (!this.IsGrounded()) return;
        if (this.IsPlayerMax()) return;
        bool objectActive = this.fire.gameObject.activeSelf;
        if (objectActive == this.actived) return;

        this.fire.gameObject.SetActive(this.actived);

        objectActive = this.fire.gameObject.activeSelf;
        this.LinkToInput(objectActive);
    }

    public override void Interact()
    {
        if (!this.IsGrounded()) return;
        if (this.IsPlayerMax()) return;

        if (!ScoreManager.instance.GoldDeduct(this.costCurrent)) return;

        PlayerManager.instance.LoadRandomPlayer();
    }

    protected virtual bool IsGrounded()
    {
        return PlayerManager.instance.playerMovement.IsGrounded();
    }

    protected virtual bool IsPlayerMax()
    {
        return PlayersHolder.instance.heroCtrls.Count >= this.playerMax;
    }
}

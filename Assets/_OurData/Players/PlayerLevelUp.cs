﻿using UnityEngine;

public class PlayerLevelUp : PlayerInteractable
{
    [Header("Player Level Up")]
    [SerializeField] protected float distance = Mathf.Infinity;
    [SerializeField] protected float distanceLimit = 1.5f;
    [SerializeField] protected bool opened = false;
    [SerializeField] protected Transform chestOpened;
    [SerializeField] protected int cost = 50;
    [SerializeField] protected Vector3 spawnPos;

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
        bool chestOpened = this.chestOpened.gameObject.activeSelf;
        if (chestOpened == this.opened) return;

        this.chestOpened.gameObject.SetActive(this.opened);

        chestOpened = this.chestOpened.gameObject.activeSelf;
        this.LinkToInput(chestOpened);
    }

    public override void Interact()
    {
        HeroCtrl currentHero = PlayerManager.instance.currentHero;
        HeroesManager heroesManager = currentHero.heroesManager;

        int currentLevel = currentHero.heroLevel.Get();
        if (!heroesManager.TryGetNextHero(currentLevel))
        {
            currentLevel = 0;
            Debug.LogWarning("Cant level up Hero");
            return;
        }

        int levelCost = this.cost * currentLevel;
        if (!ScoreManager.instance.GoldDeduct(levelCost)) return;

        HeroCtrl heroCtrl = heroesManager.GetNextHero(currentLevel);

        heroCtrl.transform.parent = PlayersHolder.instance.transform;
        ObjPoolManager.instance.Despawn(currentHero.transform);

        PlayerManager.instance.playerMovement.inputHorizontalRaw = 1;
        this.spawnPos = currentHero.transform.position;
        spawnPos.y += 0.5f;

        heroCtrl.characterCtrl.enabled = false;
        heroCtrl.transform.position = spawnPos;
        heroCtrl.characterCtrl.enabled = true;

        PlayerManager.instance.SetPlayerCtrl(heroCtrl);
    }
}

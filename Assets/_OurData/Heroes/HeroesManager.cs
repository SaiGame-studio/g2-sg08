using PathologicalGames;
using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : SaiBehaviour
{
    [Header("Hero")]
    public HeroProfile heroProfile;
    public List<HeroCtrl> heroes = new List<HeroCtrl>();
    [SerializeField] protected string poolName = "ObjPool";
    [SerializeField] protected SpawnPool pool;
    [SerializeField] protected List<PrefabPool> prefabPools;

    private void Awake()
    {
        this.LoadComponents();
        this.AddObjToPool();
    }

    protected override void LoadComponents()
    {
        this.LoadPool();
        this.LoadHeros();
        this.LoadHeroProfile();
    }

    protected virtual void LoadHeros()
    {
        if (this.heroes.Count > 0) return;

        foreach (Transform child in transform)
        {
            HeroCtrl heroCtrl = child.GetComponent<HeroCtrl>();
            this.heroes.Add(heroCtrl);
            heroCtrl.gameObject.SetActive(false);
        }

        Debug.Log(transform.name + ": LoadHeros");
    }

    protected virtual void LoadHeroProfile()
    {
        if (this.heroProfile != null) return;
        this.heroProfile = GetComponent<HeroProfile>();
        Debug.Log(transform.name + ": LoadHeroProfile");
    }

    public virtual HeroCtrl GetHero()
    {
        return this.GetHero(0);
    }

    public virtual HeroCtrl GetHero(int index)
    {
        if (index >= this.heroes.Count) return null;

        GameObject heroObj = this.heroes[index].gameObject;

        //GameObject hero = Instantiate(heroObj);
        Transform hero = ObjPoolManager.instance.Spawn(heroObj.name);

        HeroCtrl heroCtrl = hero.GetComponent<HeroCtrl>();
        heroCtrl.heroesManager = this;
        return heroCtrl;
    }

    public virtual HeroCtrl GetNextHero(int currentLevel)
    {
        return this.GetHero(currentLevel);
    }

    public virtual bool TryGetNextHero(int index)
    {
        if (index >= this.heroes.Count) return false;
        return true;
    }

    protected virtual void LoadPool()
    {
        if (this.pool != null) return;

        GameObject obj = GameObject.Find(this.poolName);
        this.pool = obj.GetComponent<SpawnPool>();
        this.pool.poolName = this.poolName;
        Debug.Log(transform.name + ": LoadPool");
    }

    protected virtual void AddObjToPool()
    {
        foreach (HeroCtrl heroCtrl in this.heroes)
        {
            PrefabPool prefabPool = new PrefabPool(heroCtrl.transform);

            bool isAlreadyPool = this.pool.GetPrefabPool(prefabPool.prefab) == null ? false : true;
            if (!isAlreadyPool)
            {
                this.pool.CreatePrefabPool(prefabPool);
                this.prefabPools.Add(prefabPool);
            }
        }
    }
}

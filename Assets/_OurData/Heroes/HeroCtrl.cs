using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using UnityEngine;

public class HeroCtrl : SaiBehaviour
{
    [Header("Hero")]
    public HeroesManager heroManagers;
    public Character character;
    public CharacterController characterCtrl;
    public Firearm firearm;
    public FirearmFire firearmFire;
    public Transform armL;
    public Transform armR;
    public HeroProfile heroProfile;
    public HeroLevel heroLevel;

    private void OnEnable()
    {
        this.FixCharacter();
    }

    protected override void LoadComponents()
    {
        this.LoadCharacter();
        this.LoadCharCtrl();
        this.LoadCharBodyParts();
        this.LoadHeroProfile();
        this.SetLayer();
        this.LoadHeroLevel();
    }

    protected virtual void LoadHeroLevel()
    {
        if (this.heroLevel != null) return;
        this.heroLevel = GetComponent<HeroLevel>();
        Debug.Log(transform.name + ": LoadHeroLevel");
    }

    protected virtual void SetLayer()
    {
        if (MyLayerManager.instance == null) return;
        if (gameObject.layer != 0) return;
        gameObject.layer = MyLayerManager.instance.layerHero;
        Debug.LogWarning(transform.name + ": Setlayer");
    }

    protected virtual void LoadHeroProfile()
    {
        if (this.heroProfile != null) return;
        this.heroProfile = GetComponent<HeroProfile>();
        Debug.Log(transform.name + ": LoadHeroProfile");
    }

    protected virtual void LoadCharCtrl()
    {
        if (this.characterCtrl != null) return;

        this.characterCtrl = GetComponent<CharacterController>();
        if (this.characterCtrl == null) this.characterCtrl = gameObject.AddComponent<CharacterController>();

        this.characterCtrl.center = new Vector3(0, 1.125f);
        this.characterCtrl.height = 2.5f;
        this.characterCtrl.radius = 0.75f;
        this.characterCtrl.minMoveDistance = 0;

        Debug.Log(transform.name + ": LoadCharCtrl");
    }

    protected virtual void LoadCharacter()
    {
        if (this.character != null) return;
        this.character = GetComponentInChildren<Character>();

        Debug.Log(transform.name + ": LoadCharacter");
    }

    protected virtual void LoadCharBodyParts()
    {
        if (this.firearm != null) return;

        Transform animation = this.character.transform.Find("Animation");
        Transform body = animation.Find("Body");
        Transform upper = body.Find("Upper");
        Transform armR1 = upper.Find("ArmR[1]");
        Transform forearmR = armR1.Find("ForearmR");
        Transform randR = forearmR.Find("HandR");
        Transform tranFirearm = randR.Find("Firearm");

        this.armL = upper.Find("ArmL");
        this.armR = armR1;
        this.firearm = tranFirearm.GetComponent<Firearm>();
        this.firearmFire = this.firearm.transform.GetComponent<FirearmFire>();
        this.firearmFire.CreateBullets = true;

        Debug.Log(transform.name + ": LoadCharBodyParts");
    }

    protected virtual void FixCharacter()
    {
        this.firearmFire.CreateBullets = true;
    }
}

using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCtrl : MonoBehaviour
{
    [Header("Hero")]
    public string charName;
    public Character character;
    public CharacterController characterCtrl;
    public Firearm firearm;
    public FirearmFire firearmFire;
    public Transform armL;
    public Transform armR;

    private void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadCharacter();
        this.LoadCharCtrl();
        this.LoadCharBodyParts();
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
        this.character = GetComponent<Character>();

        Debug.Log(transform.name + ": LoadCharactor");
    }

    protected virtual void LoadCharBodyParts()
    {
        if (this.firearm != null) return;

        Transform animation = transform.Find("Animation");
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

        Debug.Log(transform.name+ ": LoadCharBodyParts");
    }
}

using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCtrl : MonoBehaviour
{
    [Header("Hero")]
    public Character character;
    public CharacterController characterCtrl;

    private void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadCharacter();
        this.LoadCharCtrl();
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
}

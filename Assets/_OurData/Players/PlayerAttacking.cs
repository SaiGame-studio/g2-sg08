using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using Assets.HeroEditor.Common.ExampleScripts;
using HeroEditor.Common.Enums;
using System;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    [Header("Attack")]
    public bool attacking = false;
    [SerializeField] protected float attackSpeed = 0.5f;
    [SerializeField] protected float finalAttackSpeed = 0.5f;
    [SerializeField] protected float attackSpeedMax = 0.1f;
    [SerializeField] protected float attackTimer = Mathf.Infinity;

    [Header("Hero Editor")]
    public Character character;
    public BowExample BowExample;
    public Firearm firearm;
    public Transform armL;
    public Transform armR;
    public KeyCode FireButton = KeyCode.Mouse0;
    public KeyCode ReloadButton = KeyCode.R;
    public bool FixedArm;

    protected void FixedUpdate()
    {
        this.Attacking();
    }

    public void Update()
    {
        if (character.Animator.GetInteger("State") >= (int)CharacterState.DeathB) return;

        switch (character.WeaponType)
        {
            case WeaponType.Melee1H:
            case WeaponType.Melee2H:
            case WeaponType.MeleePaired:
                if (Input.GetKeyDown(FireButton)) this.character.Slash();
                break;
            case WeaponType.Bow:
                BowExample.ChargeButtonDown = Input.GetKeyDown(FireButton);
                BowExample.ChargeButtonUp = Input.GetKeyUp(FireButton);
                break;
            case WeaponType.Firearms1H:
            case WeaponType.Firearms2H:
                firearm.Fire.FireButtonDown = Input.GetKeyDown(FireButton);
                firearm.Fire.FireButtonPressed = Input.GetKey(FireButton);
                firearm.Fire.FireButtonUp = Input.GetKeyUp(FireButton);
                firearm.Reload.ReloadButtonDown = Input.GetKeyDown(ReloadButton);
                break;
            case WeaponType.Supplies:
                if (Input.GetKeyDown(FireButton)) character.Animator.Play(Time.frameCount % 2 == 0 ? "UseSupply" : "ThrowSupply", 0); // Play animation randomly.
                break;
        }

        if (Input.GetKeyDown(FireButton)) character.GetReady();
    }

    /// <summary>
    /// Called each frame update, weapon to mouse rotation example.
    /// </summary>
    public void LateUpdate()
    {
        switch (character.GetState())
        {
            case CharacterState.DeathB:
            case CharacterState.DeathF:
                return;
        }

        Transform arm;
        Transform weapon;

        switch (character.WeaponType)
        {
            case WeaponType.Bow:
                arm = armL;
                weapon = character.BowRenderers[3].transform;
                break;
            case WeaponType.Firearms1H:
            case WeaponType.Firearms2H:
                arm = armR;
                weapon = firearm.FireTransform;
                break;
            default:
                return;
        }

        RotateArm(arm, weapon, FixedArm ? arm.position + 1000 * Vector3.right : Camera.main.ScreenToWorldPoint(Input.mousePosition), -40, 40);
    }

    protected virtual void Attacking()
    {
        if (!this.attacking) return;
        this.attackTimer += Time.fixedDeltaTime;
        this.GetAttackSpeed();
        if (this.attackTimer < this.finalAttackSpeed) return;
        this.attackTimer = 0;

        PlayerManager.instance.currentHero.AutoAttack();
    }

    protected virtual float GetAttackSpeed()
    {
        this.finalAttackSpeed = this.attackSpeed;
        float level = PlayerManager.instance.currentHero.heroLevel.Get();
        level *= 2;
        level = (float)level / 100;
        this.finalAttackSpeed -= level;

        return finalAttackSpeed;
    }

    /// <summary>
    /// Selected arm to position (world space) rotation, with limits.
    /// </summary>
    public void RotateArm(Transform arm, Transform weapon, Vector2 target, float angleMin, float angleMax) // TODO: Very hard to understand logic.
    {
        target = arm.transform.InverseTransformPoint(target);

        var angleToTarget = Vector2.SignedAngle(Vector2.right, target);
        var angleToFirearm = Vector2.SignedAngle(weapon.right, arm.transform.right) * Math.Sign(weapon.lossyScale.x);
        var fix = weapon.InverseTransformPoint(arm.transform.position).y / target.magnitude;

        if (fix < -1) fix = -1;
        if (fix > 1) fix = 1;

        var angleFix = Mathf.Asin(fix) * Mathf.Rad2Deg;
        var angle = angleToTarget + angleToFirearm + angleFix;

        angleMin += angleToFirearm;
        angleMax += angleToFirearm;

        var z = arm.transform.localEulerAngles.z;

        if (z > 180) z -= 360;

        if (z + angle > angleMax)
        {
            angle = angleMax;
        }
        else if (z + angle < angleMin)
        {
            angle = angleMin;
        }
        else
        {
            angle += z;
        }

        if (float.IsNaN(angle))
        {
            Debug.LogWarning(angle);
        }

        arm.transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}

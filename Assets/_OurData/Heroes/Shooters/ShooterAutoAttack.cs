using System;
using UnityEngine;

public class ShooterAutoAttack : PlayerAutoAttack
{

    public void LateUpdate()
    {
        this.AutoArm();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.attackRange = 7;
    }

    protected virtual void AutoArm()
    {
        if (this.target == null) return;

        Transform arm;
        Transform weapon;
        bool FixedArm = false;
        Vector3 targetDirection = this.target.position;

        arm = this.heroCtrl.armR;
        weapon = this.heroCtrl.firearm.FireTransform;

        RotateArm(arm, weapon, FixedArm ? arm.position + 1000 * Vector3.right : targetDirection, -40, 40);
    }

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

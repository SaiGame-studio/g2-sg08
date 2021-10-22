using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillCtrl : SkillCtrl
{
    private void OnEnable()
    {
        Invoke("Despawn", 0.1f);
    }

    public void Update()
    {
        //this.Turning();
    }

    protected virtual void Turning()
    {
        Vector3 mouseToChar = PlayerManager.instance.playerMovement.MouseToChar();
        Vector3 localScale = this.skill.transform.localScale;
        localScale.x = Mathf.Sign(mouseToChar.x);
        this.skill.transform.localScale = localScale;
    }

    public virtual void Turn(Vector3 direction)
    {
        Vector3 localScale = this.skill.transform.localScale;
        localScale.x = Mathf.Sign(direction.x);
        this.skill.transform.localScale = localScale;
    }

    protected virtual void Despawn()
    {
        ObjPoolManager.instance.Despawn(transform);
    }
}

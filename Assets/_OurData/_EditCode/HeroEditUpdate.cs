using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using UnityEngine;

public class HeroEditUpdate : ScriptableObject
{
    public Firearm Firearm;

    private void FirearmFireUpdate()
    {
        //File: FirearmFire
        //Line: 192

        //var bullet = Instantiate(Firearm.Params.ProjectilePrefab, Firearm.FireTransform);
        //SaiModify
        Transform tBullet = ObjPoolManager.instance.Spawn("Bullet", Firearm.FireTransform);
        Bullet bullet = tBullet.GetComponent<Bullet>();
        tBullet.gameObject.SetActive(true);
    }
}
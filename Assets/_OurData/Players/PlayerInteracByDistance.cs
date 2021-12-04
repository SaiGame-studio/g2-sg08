using UnityEngine;

public class PlayerInteracByDistance : PlayerInteractable
{
    [Header("Distance")]
    [SerializeField] protected float distance = Mathf.Infinity;
    [SerializeField] protected float distanceLimit = 1.5f;
    [SerializeField] protected bool actived = false;

    private void FixedUpdate()
    {
        this.CheckDistance();
    }

    protected virtual void CheckDistance()
    {
        HeroCtrl hero = PlayerManager.instance.currentHero;
        this.distance = Vector3.Distance(transform.position, hero.transform.position);

        this.actived = false;
        if (this.distance > this.distanceLimit) return;

        this.actived = true;
    }
}

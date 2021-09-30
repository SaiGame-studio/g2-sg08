using UnityEngine;

public class PlayerLevelUp : SaiBehaviour
{
    [Header("Player Level Up")]
    [SerializeField] protected float distance = Mathf.Infinity;
    [SerializeField] protected float distanceLimit = 1.5f;

    private void FixedUpdate()
    {
        this.CheckDistance();
    }

    protected virtual void CheckDistance()
    {
        HeroCtrl hero = PlayerManager.instance.currentHero;
        this.distance = Vector3.Distance(transform.position, hero.transform.position);
    }
}

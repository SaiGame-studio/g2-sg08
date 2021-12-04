using UnityEngine;

public class PlayerAutoAttack : SaiBehaviour
{
    [Header("Auto Attack")]
    public HeroCtrl heroCtrl;
    [SerializeField] protected Transform target;
    [SerializeField] protected float attackRange = 2f;
    [SerializeField] protected float attackSpeed = 1f;
    [SerializeField] protected float attackTimer;
    [SerializeField] protected float targetDis = Mathf.Infinity;
    [SerializeField] protected Vector3 targetDir;

    private void FixedUpdate()
    {
        this.TargetFinding();
        this.Turning();
        this.IsTargetTooFar();
    }

    private void Update()
    {
        this.Attacking();
    }

    private void OnDrawGizmos()
    {
        this.ShowTargetZone();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHeroCtrl();
    }

    protected virtual void LoadHeroCtrl()
    {
        if (this.heroCtrl != null) return;
        this.heroCtrl = transform.parent.GetComponent<HeroCtrl>();
        Debug.Log(transform.name + ": LoadHeroCtrl");
    }

    protected virtual void ShowTargetZone()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, this.attackRange);
    }

    protected virtual void IsTargetTooFar()
    {
        if (this.target == null) return;
        if (!this.target.gameObject.activeSelf)
        {
            this.target = null;
            return;
        }

        this.targetDis = Vector3.Distance(transform.position, this.target.position);
        if (this.targetDis > this.attackRange) this.target = null;
        //Debug.Log(transform.name + ": IsTargetTooFar");
    }

    protected virtual void Attacking()
    {
        if (this.target == null) return;

        this.attackTimer += Time.deltaTime;
        if (this.attackTimer <= this.attackSpeed) return;
        this.attackTimer = 0;

        this.heroCtrl.AutoAttack();
        //Debug.Log(transform.parent.name + ": Attacking " + this.target.name);
    }

    protected virtual void TargetFinding()
    {
        if (this.target) return;

        //Debug.Log(transform.parent.name + ": TargetFinding");

        float dis;
        foreach (Transform obj in EnemySpawner.instance.objests)
        {
            dis = Vector3.Distance(transform.position, obj.position);
            if (dis <= this.attackRange)
            {
                this.TargetSet(obj);
                return;
            }
        }
    }

    protected virtual void TargetSet(Transform target)
    {
        this.target = target;
    }

    protected virtual void Turning()
    {
        if (this.target == null) return;
        this.targetDir = this.target.position - transform.position;
        Transform charTrans = this.heroCtrl.character.transform;

        charTrans.localScale = new Vector3(Mathf.Sign(this.targetDir.x), 1, 1);
    }

    public virtual Vector3 TargetDir()
    {
        return this.targetDir;
    }
}

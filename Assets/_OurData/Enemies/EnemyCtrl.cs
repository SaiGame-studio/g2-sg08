using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts.Tweens;
using UnityEngine;

public class EnemyCtrl : SaiBehaviour
{
    [Header("Enemy")]
    public Rigidbody _rigidbody;
    public Collider _collider;
    public Transform enemy;
    public EnemyMovement enemyMovement;
    public DamageReceiver damageReceiver;
    public EnemyLevel enemyLevel;
    public ScaleSpring scaleSpring;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadRigibody();
        this.LoadEnemy();
        this.LoadEnemyMovement();
        this.LoadEnemyReceiver();
        this.LoadEnemyLevel();
        this.LoadScaleSpring();
    }

    protected virtual void LoadScaleSpring()
    {
        if (this.scaleSpring != null) return;
        this.scaleSpring = GetComponent<ScaleSpring>();
        this.scaleSpring.enabled = false;
        Debug.Log(transform.name + ": LoadScaleSpring");
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<Collider>();
        Debug.Log(transform.name + ": LoadCollider");
    }

    protected virtual void LoadEnemyReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = GetComponent<DamageReceiver>();
        Debug.Log(transform.name + ": LoadEnemyReceiver");
    }

    protected virtual void LoadEnemyLevel()
    {
        if (this.enemyLevel != null) return;
        this.enemyLevel = GetComponent<EnemyLevel>();
        Debug.Log(transform.name + ": LoadEnemyLevel");
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();

        this._rigidbody.constraints = RigidbodyConstraints.FreezePositionZ
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezeRotationZ;

        Debug.Log(transform.name + ": LoadRigibody");
    }

    protected virtual void LoadEnemy()
    {
        if (this.enemy != null) return;
        this.enemy = transform.Find("Enemy");
        Debug.Log(transform.name + ": LoadEnemy");
    }

    protected virtual void LoadEnemyMovement()
    {
        Transform moveTransform = transform.Find("EnemyMovement");
        if (moveTransform == null) return;

        if (this.enemyMovement != null) return;
        if (moveTransform) this.enemyMovement = moveTransform.GetComponent<EnemyMovement>();
        Debug.Log(transform.name + ": LoadEnemy");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : SaiBehaviour
{
    [Header("Pet")]
    [SerializeField] protected PetCtrl petCtrl;
    [SerializeField] protected Transform target;
    public Transform Target => target;

    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float currentDis = Mathf.Infinity;
    [SerializeField] protected float limitDis = 2f;
    public float CurrentDis => currentDis;
    public float LimitDis => limitDis;

    [SerializeField] protected Vector3 direction = Vector3.zero;
    [SerializeField] protected Vector3 startPosition = Vector3.zero;

    private void Start()
    {
        this.startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        this.GetCurrentDis();
        this.GetDirection();
        this.Moving();
        this.Turning();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.petCtrl != null) return;
        this.petCtrl = transform.parent.GetComponent<PetCtrl>();
        Debug.LogWarning(transform.name + ": LoadEnemyCtrl");
    }

    protected virtual void GetCurrentDis()
    {
        this.currentDis = Vector3.Distance(transform.position, this.target.position);
    }

    protected virtual void Moving()
    {
        if (!this.IsTargetActive()) return;
        if (this.currentDis < this.limitDis) return;

        float t = Time.fixedDeltaTime * this.speed;
        this.startPosition = transform.position;
        transform.parent.position = Vector2.Lerp(this.startPosition, this.GetTargetPositon(), t);
    }

    protected virtual Vector3 GetTargetPositon()
    {
        Vector3 pos = this.target.position;
        //pos.x += Random.Range(-5, 5);
        //pos.z += Random.Range(-5, 5);
        return pos;
    }

    protected virtual Vector3 GetDirection()
    {
        this.direction.x = 0;
        if (this.target.position.x > transform.position.x) this.direction.x = 1;
        if (this.target.position.x < transform.position.x) this.direction.x = -1;
        return this.direction;
    }

    protected virtual bool IsTargetActive()
    {
        if (this.target == null) return false;

        return true;
    }

    protected virtual void Turning()
    {
        if (this.direction.x == 0) return;

        Vector3 newScale = this.petCtrl.model.localScale;
        newScale.x = Mathf.Abs(newScale.x);
        newScale.x *= this.direction.x * -1;//TODO: need optimize

        this.petCtrl.model.localScale = newScale;
    }

    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }

    public virtual void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }

    public virtual float GetSpeed()
    {
        return this.speed;
    }
}

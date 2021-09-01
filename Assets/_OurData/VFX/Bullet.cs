using System.Collections.Generic;
using UnityEngine;

public class Bullet : SaiBehaviour
{
    public List<Renderer> Renderers;
    [SerializeField] protected GameObject trail;
    [SerializeField] protected GameObject impact;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Collider _collider;
    [SerializeField] protected bool isDespawn = false;
    [SerializeField] protected float despawnTimer = 0f;
    [SerializeField] protected float despawnDelay = 1f;
    [SerializeField] protected Vector3 lastPosition = Vector3.zero;

    private void Update()
    {
        if (_rigidbody != null && _rigidbody.useGravity)
        {
            transform.right = _rigidbody.velocity.normalized;
        }

        this.Raycasting();
    }

    private void FixedUpdate()
    {
        this.Despawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.Bang(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        this.Bang(other.gameObject);
    }

    private void Bang(GameObject other)
    {
        this.ReplaceImpactSound(other);
        this.BulletHit();

        foreach (var ps in trail.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
        }

        this.TrailStatus(false);
    }

    protected virtual void TrailStatus(bool status)
    {
        foreach (var tr in trail.GetComponentsInChildren<TrailRenderer>())
        {
            tr.enabled = status;
        }
    }

    protected virtual void Raycasting()
    {
        Vector3 direction = (transform.position - this.lastPosition).normalized;
        Vector3 position = transform.position;
        Physics.Raycast(position, direction, out RaycastHit hit);
        this.DebugRaycast(position, hit, direction);
        this.lastPosition = transform.position;

        if (hit.transform == null) return;

        int hitLayer = hit.transform.gameObject.layer;

        Collider hitCollider = hit.transform.GetComponent<Collider>();
        Physics.IgnoreCollision(this._collider, hitCollider, true);

        if (hitLayer == MyLayerManager.instance.layerHero) return;
        if (hitLayer == MyLayerManager.instance.layerCeiling) return;

        Physics.IgnoreCollision(this._collider, hitCollider, false);
    }


    private void ReplaceImpactSound(GameObject other)
    {
        var sound = other.GetComponent<AudioSource>();

        if (sound != null && sound.clip != null)
        {
            impact.GetComponent<AudioSource>().clip = sound.clip;
        }
    }

    protected virtual void BulletHit()
    {
        this.impact.SetActive(true);
        this.spriteRenderer.enabled = false;
        this._collider.enabled = false;
        this._rigidbody.isKinematic = true;
        this.isDespawn = true;
    }

    protected virtual void Despawn()
    {
        if (!this.isDespawn) return;
        this.despawnTimer += Time.fixedDeltaTime;
        if (this.despawnTimer < this.despawnDelay) return;

        ObjPoolManager.instance.Despawn(transform);
    }

    protected virtual void BulletRenew()
    {
        this.impact.SetActive(false);
        this.spriteRenderer.enabled = true;
        this._collider.enabled = true;
        this._rigidbody.isKinematic = false;
        this.isDespawn = false;
        this.despawnTimer = 0f;

        this.TrailStatus(true);
    }

    private void OnEnable()
    {
        this.BulletRenew();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChildren();
        this.LoadRender();
        this.LoadRigibody();
        this.LoadCollider();
    }

    protected virtual void LoadChildren()
    {
        if (this.trail != null) return;
        this.trail = transform.Find("Trail").gameObject;
        this.impact = transform.Find("Impact").gameObject;

        Debug.Log(transform.name + ": LoadChildren");
    }

    protected virtual void LoadRender()
    {
        if (this.spriteRenderer != null) return;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + ": LoadRender");
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigibody");
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<Collider>();
        Debug.Log(transform.name + ": LoadCollider");
    }
}

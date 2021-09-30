using UnityEngine;

public class EnemyGate : SaiBehaviour
{
    [Header("EnemyGate")]
    [SerializeField] protected Collider _collider;
    [SerializeField] protected int gateId = 0;
    [SerializeField] protected GameObject nextGate;
    [SerializeField] protected GameObject spawnPos;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetCollider();
        this.GetGateId();
    }

    private void OnTriggerEnter(Collider enemy)
    {
        this.MoveEnemy(enemy.gameObject);
    }

    protected virtual void GetCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<Collider>();
        this._collider.isTrigger = true;
        Debug.Log(transform.name + ": GetCollider");
    }

    protected virtual void GetGateId()
    {
        if (this.gateId > 0) return;
        string name = gameObject.name;
        name = name.Substring(name.Length - 1, 1);
        this.gateId = int.Parse(name);

        int nextId = this.gateId + 1;
        string gateName = "EnemyGate" + nextId;
        this.nextGate = GameObject.Find(gateName);

        string spawnName = "SpawnPos" + nextId;
        this.spawnPos = GameObject.Find(spawnName);

        Debug.Log(transform.name + ": GetGateId");
    }

    protected virtual void MoveEnemy(GameObject enemy)
    {
        //Debug.Log("MoveEnemy: " + enemy.name);
        int layer = enemy.layer;
        if (layer != MyLayerManager.instance.layerEnemy) return;

        enemy.transform.position = this.spawnPos.transform.position;
        EnemyCtrl enemyCtrl = enemy.GetComponent<EnemyCtrl>();

        if(this.nextGate) enemyCtrl.enemyMovement.SetTarget(this.nextGate.transform);
    }

}

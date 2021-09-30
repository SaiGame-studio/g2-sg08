using UnityEngine;

public class EnemyLevel : Level
{
    [Header("Enemy")]
    public EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = GetComponent<EnemyCtrl>();

        Debug.Log(transform.name + ": LoadEnemyCtrl");
    }
}

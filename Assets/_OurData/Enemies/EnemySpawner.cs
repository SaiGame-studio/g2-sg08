using UnityEngine;

public class EnemySpawner : SaiBehaviour
{
    [SerializeField] protected string enemyName = "Cube";

    private void Start()
    {
        this.Spawning();
    }

    protected virtual void Spawning()
    {
        float x = Random.Range(-7.0f, 7.0f);
        float y = Random.Range(3.0f, 6.0f);

        Vector3 spawnPos = new Vector3(x, y, 0);
        Debug.Log(spawnPos.ToString());

        Transform obj = ObjPoolManager.instance.Spawn(this.enemyName, spawnPos, transform.rotation, transform);
        obj.gameObject.SetActive(true);

        Invoke("Spawning", 2);
    }
}

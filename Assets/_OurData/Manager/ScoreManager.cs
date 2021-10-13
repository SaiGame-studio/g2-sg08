using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Score")]
    [SerializeField] protected int gold = 0;
    [SerializeField] protected int kill = 0;

    private void Awake()
    {
        if (ScoreManager.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        ScoreManager.instance = this;
    }

    public virtual void Kill()
    {
        this.kill++;
    }

    public virtual int GetKill()
    {
        return this.kill;
    }

    public virtual void GoldAdd(int count)
    {
        this.gold += count;
    }

    public virtual bool GoldDeduct(int count)
    {
        if (this.gold < count) return false;
        this.gold -= count;
        return true;
    }

    public virtual int GetGold()
    {
        return this.gold;
    }

    public virtual void FromJson(string jsonString)
    {
        ScoreData obj = JsonUtility.FromJson<ScoreData>(jsonString);
        if (obj == null) return;
        this.kill = obj.kill;
        this.gold = obj.gold;
    }
}

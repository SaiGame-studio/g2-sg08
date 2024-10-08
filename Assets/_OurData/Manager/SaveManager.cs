﻿using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private const string SAVE_1 = "save_1";
    private const string SAVE_2 = "save_2";
    private const string SAVE_3 = "save_3";

    private void Awake()
    {
        if (SaveManager.instance != null) Debug.LogError("Only 1 SaveManager allow");
        SaveManager.instance = this;
    }

    private void Start()
    {
        this.LoadSaveGame();
    }

    private void OnApplicationQuit()
    {
        this.SaveGame();
    }

    protected virtual string GetSaveName()
    {
        return SaveManager.SAVE_1;
    }

    public virtual void LoadSaveGame()
    {
        string jsonString = SaveSystem.GetString(this.GetSaveName());
        ScoreManager.instance.FromJson(jsonString);
    }

    public virtual void SaveGame()
    {
        string jsonString = JsonUtility.ToJson(ScoreManager.instance);
        SaveSystem.SetString(this.GetSaveName(), jsonString);
    }
}

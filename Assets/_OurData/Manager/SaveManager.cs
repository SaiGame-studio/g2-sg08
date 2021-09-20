using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private const string SAVE_1 = "save_1";

    private void Awake()
    {
        if (SaveManager.instance != null) Debug.LogError("Only 1 SaveManager allow");
        SaveManager.instance = this;
    }

    private void Start()
    {
        this.LoadSaveGame();
    }

    protected virtual string GetSaveName()
    {
        return SaveManager.SAVE_1;
    }

    public virtual void LoadSaveGame()
    {
        string stringSave = SaveSystem.GetString(this.GetSaveName());
        Debug.Log("LoadSaveGame: " + stringSave);
    }

    public virtual void SaveGame()
    {
        Debug.Log("SaveGame");
        string stringSave = "abc stringSave ssss";
        SaveSystem.SetString(this.GetSaveName(), stringSave);
    }

    public virtual void Saving()
    {
        this.SaveGame();
        Invoke("Saving", 2f);
    }

    private void OnApplicationQuit()
    {
        this.Saving();
    }
}

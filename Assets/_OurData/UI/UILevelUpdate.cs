using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelUpdate : SaiBehaviour
{
    public TextMeshProUGUI textLevel;

    private void FixedUpdate()
    {
        this.Updateing();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    protected virtual void LoadText()
    {
        if (this.textLevel != null) return;
        this.textLevel = GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ": LoadText");
    }

    protected virtual void Updateing()
    {
        int level = GameLevelManager.instance.Level();
        this.textLevel.text = level.ToString("N0");
    }
}

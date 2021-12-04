using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGoldUpdate : SaiBehaviour
{
    public TextMeshProUGUI textGold;

    private void FixedUpdate()
    {
        this.GoldUpdating();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    protected virtual void LoadText()
    {
        if (this.textGold != null) return;
        this.textGold = GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ": LoadText");
    }

    protected virtual void GoldUpdating()
    {
        int gold = ScoreManager.instance.GetGold();
        this.textGold.text = gold.ToString("N0");
    }
}

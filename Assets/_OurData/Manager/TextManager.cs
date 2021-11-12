using DamageNumbersPro;
using UnityEngine;

public class TextManager : SaiBehaviour
{
    public static TextManager instance;
    [SerializeField] protected DamageNumber textGold;

    private void Awake()
    {
        if (TextManager.instance != null) Debug.LogError("Only 1 TextManager allow");
        TextManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTexts();
    }

    protected virtual void LoadTexts()
    {
        if (this.textGold != null) return;
        Transform tGold = transform.Find("TextGold");
        this.textGold = tGold.GetComponent<DamageNumber>();
        tGold.gameObject.SetActive(false);

        Debug.Log(transform.name + "LoadTexts");
    }

    public virtual DamageNumber TextGold(int damage, Vector3 position)
    {
        return Text(this.textGold, damage, position);
    }

    public virtual DamageNumber Text(DamageNumber dn, int damage, Vector3 position)
    {
        DamageNumber newDamageNumber = dn.CreateNew(damage, position);

        newDamageNumber.transform.gameObject.SetActive(true);
        newDamageNumber.transform.position = position;
        return newDamageNumber;
    }
}

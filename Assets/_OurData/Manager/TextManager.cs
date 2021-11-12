using DamageNumbersPro;
using UnityEngine;

public class TextManager : SaiBehaviour
{
    public static TextManager instance;

    private void Awake()
    {
        if (TextManager.instance != null) Debug.LogError("Only 1 TextManager allow");
        TextManager.instance = this;
    }

    public virtual Transform TextCombining(int damage, Vector3 position)
    {
        return Text("TextCombining", damage, position);
    }

    public virtual Transform TextGold(int damage, Vector3 position)
    {
        return Text("TextGold", damage, position);
    }

    public virtual Transform Text(string textName, int damage, Vector3 position)
    {
        Transform trans = ObjPoolManager.instance.Spawn(textName);
        DamageNumber dn = trans.GetComponent<DamageNumber>();
        dn.number = damage;

        trans.gameObject.SetActive(true);
        trans.position = position;
        return trans;
    }
}

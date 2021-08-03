using UnityEngine;

public class UIChoosePlayer : MonoBehaviour
{
    public virtual void ChoosePlayer()
    {
        string heroClass = transform.name.Replace("BtnChoose", "");
        PlayerManager.instance.ChoosePlayer(heroClass);
    }
}

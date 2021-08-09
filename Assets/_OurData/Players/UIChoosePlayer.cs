using UnityEngine;

public class UIChoosePlayer : MonoBehaviour
{
    public virtual void ChoosePlayer()
    {
        string heroClass = transform.name.Replace("BtnChoose", "");
        //Debug.Log(transform.name + ": " + heroClass);
        PlayerManager.instance.ChoosePlayer(heroClass);
    }
}

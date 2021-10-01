using UnityEngine;

public class PlayerInteractable : SaiBehaviour
{
    public virtual void Interact()
    {
        Debug.Log(transform.name + ": Interacting");
    }

    public virtual void LinkToInput(bool link)
    {
        PlayerManager playerManager = PlayerManager.instance;
        if (link) playerManager.playerInput.interactable = this;
        else playerManager.playerInput.interactable = null;
    }
}

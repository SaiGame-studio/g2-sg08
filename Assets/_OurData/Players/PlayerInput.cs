using UnityEngine;

public class PlayerInput : SaiBehaviour
{
    [Header("Player Input")]
    public PlayerInteractable interactable;

    private void Update()
    {
        this.Interacting();
    }

    protected virtual void Interacting()
    {
        if (this.interactable == null) return;

        if (Input.GetKeyDown("f")) this.interactable.Interact();
    }

}

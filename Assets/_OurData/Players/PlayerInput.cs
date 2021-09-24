using UnityEngine;

public class PlayerInput : SaiBehaviour
{
    [Header("Player Input")]
    public PlayerInteractable interactable;

    private void Update()
    {
        this.Interacting();
        this.Moving();
    }

    protected virtual void Interacting()
    {
        if (this.interactable == null) return;

        if (Input.GetKeyDown("f")) this.interactable.Interact();
    }

    protected virtual void Moving()
    {
        PlayerMovement playerMovement = PlayerManager.instance.playerMovement;
        playerMovement.inputHorizontalRaw = Input.GetAxisRaw("Horizontal");
        playerMovement.inputVerticalRaw = Input.GetAxisRaw("Vertical");
        playerMovement.inputJumbRaw = Input.GetAxisRaw("Jump");
    }
}

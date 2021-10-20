using UnityEngine;

public class PlayerInput : SaiBehaviour
{
    [Header("Player Input")]
    public PlayerInteractable interactable;

    private void Update()
    {
        this.Interacting();
        this.Moving();
        this.ChoosePlayer();
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

    protected virtual void ChoosePlayer()
    {
        int playerIndex = 0;

        if (Input.GetKeyUp(KeyCode.Alpha1)) playerIndex = 1;
        if (Input.GetKeyUp(KeyCode.Alpha2)) playerIndex = 2;
        if (Input.GetKeyUp(KeyCode.Alpha3)) playerIndex = 3;
        if (Input.GetKeyUp(KeyCode.Alpha4)) playerIndex = 4;
        if (Input.GetKeyUp(KeyCode.Alpha5)) playerIndex = 5;
        if (Input.GetKeyUp(KeyCode.Alpha6)) playerIndex = 6;
        if (Input.GetKeyUp(KeyCode.Alpha7)) playerIndex = 7;

        if (playerIndex > 0) PlayerManager.instance.ChoosePlayer(playerIndex);
    }
}

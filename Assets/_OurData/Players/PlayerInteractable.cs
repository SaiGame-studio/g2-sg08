using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : SaiBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Interacting");
    }
}

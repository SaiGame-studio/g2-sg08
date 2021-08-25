using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : SaiBehaviour
{
    public virtual void ChangeLayer(int layerInt)
    {
        gameObject.layer = layerInt;
    }
}

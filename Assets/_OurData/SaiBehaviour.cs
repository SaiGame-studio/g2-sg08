using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiBehaviour : MonoBehaviour
{
    [SerializeField] protected bool debug = false;

    private void Awake()
    {
        this.LoadComponents();
    }

    private void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        //Debug.Log("Need overide");
    }

    protected virtual void DebugRaycast(Vector3 start, RaycastHit hit, Vector3 direction)
    {
        if (!this.debug) return;

        if (hit.transform == null)
        {
            Debug.DrawRay(start, direction, Color.red);
            Debug.Log(transform.name + ": Hit Nothing");
        }
        else
        {
            Debug.DrawLine(start, hit.point, Color.green);
            Debug.Log(transform.name + ": Hit " + hit.transform.name);
        }
    }
}

using System;
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
        this.ResetValue();
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        //Debug.Log("Need overide");
    }

    protected virtual void ResetValue()
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

    protected string PrettyCurrency(long cash, string prefix = "$")
    {
        string[] suffixes = { "", "k", "M", "G" };

        int k;
        if (cash == 0)
            k = 0;    // log10 of 0 is not valid
        else
            k = (int)(Math.Log10(cash) / 3); // get number of digits and divide by 3
        var dividor = Math.Pow(10, k * 3);  // actual number we print
        var text = prefix + (cash / dividor).ToString("F1") + suffixes[k];
        return text;
    }
}

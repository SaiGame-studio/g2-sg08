using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroProfile : MonoBehaviour
{
    [SerializeField] protected string heroClass = "Hero";

    public virtual string HeroClass()
    {
        return this.heroClass;
    }
}

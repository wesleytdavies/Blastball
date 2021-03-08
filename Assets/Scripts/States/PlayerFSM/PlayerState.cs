using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Leave() { }
}

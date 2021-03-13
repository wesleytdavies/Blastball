using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public virtual void Enter(Player player) { }
    public virtual void Update(Player player) { }
    public virtual void Leave(Player player) { }
}

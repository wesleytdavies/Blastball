using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcState
{
    public virtual void Enter(Npc npc) { }
    public virtual void Update(Npc npc) { }
    public virtual void Leave(Npc npc) { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlastballState
{
    public abstract void Enter(Blastball blastball);
    public abstract void Update(Blastball blastball);
    public abstract void Leave(Blastball blastball);
}

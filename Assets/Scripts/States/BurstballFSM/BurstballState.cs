using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BurstballState
{
    public abstract void Enter(Burstball burstball);
    public abstract void Update(Burstball burstball);
    public abstract void Leave(Burstball burstball);
}

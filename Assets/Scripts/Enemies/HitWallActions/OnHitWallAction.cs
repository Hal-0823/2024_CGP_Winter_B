using UnityEngine;
using System;

public abstract class OnHitWallAction : ScriptableObject
{
    public abstract void Execute(BullController bull, Collision col);
}
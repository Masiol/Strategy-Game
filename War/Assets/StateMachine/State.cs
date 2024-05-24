using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract void Enter(Unit unit);
    public abstract void Execute(Unit unit);
    public abstract void Exit(Unit unit);
}
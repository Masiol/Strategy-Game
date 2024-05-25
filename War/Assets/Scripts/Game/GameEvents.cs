using System;
using UnityEngine;

public static class GameEvents
{
    // Event przekazuj¹cy referencjê do SquadManagera
    public static Action<SquadManager> OnSquadSpawned = delegate { };

    public static event Action<SquadManager> OnSquadSelected = delegate { };
    public static event Action OnSquadDeselected = delegate { };

    public static event Action StartGame;

    public static void SelectSquad(SquadManager squad)
    {
        OnSquadSelected(squad);
    }

    public static void DeselectSquad()
    {
        OnSquadDeselected();
    }
    public static void Start()
    {
        StartGame?.Invoke();
    }
}


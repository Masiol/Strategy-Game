using UnityEngine;

public class FormationManager : MonoBehaviour
{
    private SquadManager currentSquadManager;
    private void OnEnable()
    {
        GameEvents.OnSquadSpawned += SetCurrentSquadManager;
        GameEvents.OnSquadSelected += SetCurrentSquadManager;
        GameEvents.OnSquadDeselected += NoCurrentSquadManager;
    }

    private void OnDisable()
    {
        GameEvents.OnSquadSpawned -= SetCurrentSquadManager;
        GameEvents.OnSquadSelected -= SetCurrentSquadManager;
        GameEvents.OnSquadDeselected -= NoCurrentSquadManager;
    }

    private void SetCurrentSquadManager(SquadManager _squadManager)
    {
        currentSquadManager = _squadManager;
    }
    private void NoCurrentSquadManager()
    {
        currentSquadManager = null;
    }

    public void SetFormation(IFormationStrategy _formationStrategy)
    {
        if (currentSquadManager != null)
        {
            currentSquadManager.SetFormationStrategy(_formationStrategy);
        }
    }
}

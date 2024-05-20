using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public UnitSquad squadConfig;
    private IFormationStrategy currentFormationStrategy;
    private ColliderChecker colliderChecker;

    public bool isGrounded;

    private void Awake()
    {
        switch (squadConfig.formation)
        {
            case Formation.Square:
                break;
            case Formation.Circle:
                currentFormationStrategy = new CircleFormation();
                break;
            case Formation.Triangle:
                break;
            case Formation.Line:
                currentFormationStrategy = new LineFormation();
                break;
        }
        colliderChecker = GetComponent<ColliderChecker>();
        CreateSquad();
    }

    public void SetFormationStrategy(IFormationStrategy _strategy)
    {
        currentFormationStrategy = _strategy;
        if (transform.childCount > 0) 
        {
            currentFormationStrategy.ReArrangeUnits(transform, squadConfig.spacing);
            colliderChecker.UpdateColliderBounds();

        }
    }

    public void CreateSquad()
    {
        if (squadConfig == null || currentFormationStrategy == null) return;

        ClearExistingUnits();
        currentFormationStrategy.ArrangeUnits(squadConfig.unitPrefab, squadConfig.unitCount, transform, squadConfig.spacing);
        colliderChecker.UpdateColliderBounds();
        isGrounded = true;
        GameEvents.OnSquadSpawned(this);

    }

    private void ClearExistingUnits()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormationArmy : MonoBehaviour
{
    public delegate void FormationCompletedEventHandler();
    public event FormationCompletedEventHandler FormationCompleted;

    private FormationBase formation;
    private FormationBase previousFormation;
    public FormationBase Formation
    {
        get
        {
            if (formation == null) formation = GetComponent<FormationBase>();
            return formation;
        }
        set => formation = value;
    }

    [SerializeField] private float unitSpeed;

    private List<GameObject> spawnedUnits = new List<GameObject>();
    private List<Vector3> points = new List<Vector3>();
    private List<Vector3> newPositions = new List<Vector3>();
    private Vector3[] initialUnitPositions;
    private Vector3 newDestination;
    private int unitsReachedDestination = 0;
    private bool applyNewFormation;

    private void Start()
    {
        spawnedUnits = GetComponent<FormCreator>().units;
        SelectFormation(false);
        SetUnitOffsets();
    }

    private void Update()
    {
        if (applyNewFormation)
        {
            if (Formation != previousFormation)
            {
                previousFormation = Formation;
                SetFormation();
            }
        }
    }

    private void SetFormation()
    {
        points = Formation.EvaluatePoints().ToList();

        for (var i = 0; i < spawnedUnits.Count; i++)
        {
            if (i < points.Count)
            {
                UnitController unitController = spawnedUnits[i].GetComponent<UnitController>();
                if (unitController != null)
                {
                    float distanceToTarget = Vector3.Distance(unitController.transform.position, transform.position + points[i]);
                    if (distanceToTarget > 0.1f)
                    {
                        unitController.MoveTo(transform.position + points[i]);
                    }
                }
            }
        }
    }

   /* public void GetNewPositionsForEachUnit(Vector3 point)
    {
        newPositions.Add(point);
    }
    public void ConfigureCollider()
    {
        Vector3 minBounds = Vector3.one * Mathf.Infinity;
        Vector3 maxBounds = Vector3.one * Mathf.NegativeInfinity;

        foreach (Vector3 pos in newPositions)
        {
            Vector3 unitPosition = pos;
            minBounds = Vector3.Min(minBounds, unitPosition);
            maxBounds = Vector3.Max(maxBounds, unitPosition);
        }

        Vector3 center = (minBounds + maxBounds) * 0.5f;
        Vector3 size = maxBounds - minBounds;

        float customHeight = 5.0f;
        size.y = customHeight;

        float margin = 0.1f;
        size += Vector3.one * margin;

        boxCollider.size = size;
        boxCollider.center = center - transform.position;
    }*/

    public void UnitReachedDestination()
    {
        unitsReachedDestination++;
        if (unitsReachedDestination == spawnedUnits.Count)
        {
            OnFormationCompleted();
            unitsReachedDestination = 0;
        }
    }

    private void OnFormationCompleted()
    {
        FormationCompleted?.Invoke();
        SetUnitOffsets();
    }

    public void ChangeFormation(FormationBase _formation)
    {
        Formation = _formation;
        applyNewFormation = true;
    }

    public void SelectFormation(bool _state)
    {
        foreach (var unit in spawnedUnits)
        {
            if (unit.transform.childCount > 0)
            {
                foreach (Transform child in unit.transform)
                {
                    if (child.CompareTag("Selector"))
                    {
                        child.gameObject.SetActive(_state);
                    }
                }
            }
        }
    }
   /* public void MoveFormationTo(Vector3 destination)
    {
        Vector3 newCenter = destination;
        newDestination = destination;
        newPositions.Clear();

        Vector3 offset = newCenter - CalculateFormationCenter();
        Vector3 totalDisplacement = GetComponentInChildren<CheckMapBounds>().LastDisplacement;

        for (int i = 0; i < spawnedUnits.Count; i++)
        {
            Vector3 newPosition = spawnedUnits[i].transform.position + offset + totalDisplacement;
            newPositions.Add(newPosition);

            UnitController unitController = spawnedUnits[i].GetComponent<UnitController>();
            if (unitController != null)
            {
                unitController.MoveTo(newPosition);
            }
        }

        ConfigureCollider(); 
        newDestination = destination + totalDisplacement; 
    }*/
    private void SetUnitOffsets()
    {
        initialUnitPositions = new Vector3[spawnedUnits.Count];
        Vector3 center = transform.GetChild(0).position;

        for (int i = 0; i < spawnedUnits.Count; i++)
        {
            initialUnitPositions[i] = spawnedUnits[i].transform.position - center;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 formationCenter = CalculateFormationCenter();

        Gizmos.DrawWireSphere(formationCenter, 1f);

        foreach (var unit in spawnedUnits)
        {
            Vector3 unitPosition = unit.transform.position;
            Gizmos.DrawLine(unitPosition, formationCenter);
        }
    }

    private Vector3 CalculateFormationCenter()
    {
        Vector3 center = Vector3.zero;

        foreach (var unit in spawnedUnits)
        {
            center += unit.transform.position;
        }
        center /= spawnedUnits.Count;

        return center;
    }
}
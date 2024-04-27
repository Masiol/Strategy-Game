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

    public int Clicked;

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
                        unitController.MoveTo(new Vector3(transform.position.x, 0, transform.position.z) + points[i]);
                    }
                }
            }
        }
    }
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

    public void ChangeFormation(FormationBase _formation, FormationSettings _formationSettings)
    {
        
        Formation = _formation;
        switch (_formation)
        {
            case BoxFormation:
                Formation.ApplySettings(_formationSettings.formationsSettings.boxFormationSettings);
                break;
            case LineFormation:
                Formation.ApplySettings(_formationSettings.formationsSettings.lineFormationSettings);
                break;
            case RadialFormation:
                Formation.ApplySettings(_formationSettings.formationsSettings.circleFormationSettings);
                break;
            case TriangleFormation:
                Formation.ApplySettings(_formationSettings.formationsSettings.triangleFormationSettings);
                break;
        }      
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

    public int GetFirstClicked()
    {
        return Clicked;
    }
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
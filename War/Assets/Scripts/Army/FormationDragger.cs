using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public enum PlacementState
{
    Valid,
    Invalid
}
public class FormationDragger : MonoBehaviour
{
    private Transform selectedFormation;
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;

    private PlacementState currentState = PlacementState.Valid;
    private MaterialController materialController;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        GameEvents.OnSquadSelected += OnSquadSelected;
        GameEvents.OnSquadDeselected += OnSquadDeselected;
    }

    private void OnDisable()
    {
        GameEvents.OnSquadSelected -= OnSquadSelected;
        GameEvents.OnSquadDeselected -= OnSquadDeselected;
    }

    private void OnSquadSelected(SquadManager _squad)
    {
        selectedFormation = _squad.transform;
        materialController = _squad.GetComponent<MaterialController>();
        _squad.isGrounded = false;
        EnableDisableNavMeshAgents(false);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            offset = selectedFormation.position - hit.point;
        }
    }

    private void OnSquadDeselected()
    {
        selectedFormation = null;
        isDragging = false;
    }

     private void Update()
      {
          if (selectedFormation != null)
          {
              MoveObjectToCursor();
          }
      }
    private void LateUpdate()
    {
        if (selectedFormation != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                PlaceOrRemoveObject();
            }
        }
    }

    private void MoveObjectToCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 newPosition = hit.point + offset;
            selectedFormation.transform.DOMove(newPosition, 0.2f);
        }
    }
    private void PlaceOrRemoveObject()
    {
        if (currentState == PlacementState.Valid)
        {
            selectedFormation.GetComponent<MaterialController>().SetFinalMaterial();
            EnableDisableNavMeshAgents(true);
            selectedFormation.GetComponent<SquadManager>().isGrounded = true;
            selectedFormation = null;

        }
        else if (currentState == PlacementState.Invalid)
        {
            Destroy(selectedFormation.gameObject);
        }
    }

     public void SetState(PlacementState _placementState)
     {
         currentState = _placementState;
         UpdateMaterial();
     }

     private void UpdateMaterial()
     {
         if (materialController != null)
         {
             if (currentState == PlacementState.Valid)
                 materialController.SetValid();
             else
                 materialController.SetInvalid();
         }
     }
    private void EnableDisableNavMeshAgents(bool _state)
    {
        foreach (Transform child in selectedFormation)
        {
            var navMeshAgent = child.GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = _state;
            }
        }
    }
}

using DG.Tweening.Core.Easing;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class UnitPlacer : MonoBehaviour
{
    public static UnitPlacer instance;

    public LayerMask groundLayerMask;

    private GameObject unitPrefab;
    private GameObject toPlace;

    private Camera _mainCamera;
    private bool isPlacingUnit = false;

    private bool clickedUnit;
    private bool preparingUnit;

    private Vector3 mousePos;

    public Vector3 offset;

   // public Unit currentSelectedUnit;

    public static event Action/*<int, int>*/ OnPlacedUnit;
    public static event Action/*<int, int>*/ OnPreparedPlaceUnit;

    private void Awake()
    {
        instance = this;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isPlacingUnit)
        {
            HandleInput();
            if (preparingUnit)

                toPlace.transform.position = mousePos + offset;
        }
        RemoveUnitCheck();

    }
    public void SetUnitPrefab(GameObject _prefab)
    {
       // if (!GameManager.instance.CanPlaceNextUnit(_prefab.GetComponent<Unit>().unitSO.commandPoints)) return;
        unitPrefab = _prefab;
        isPlacingUnit = true;
        clickedUnit = true;
        OnPreparedPlaceUnit?.Invoke();
    }

    private void HandleInput()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, groundLayerMask))
        {
            if (toPlace == null && unitPrefab != null)
            {
                clickedUnit = true;
                _PrepareUnit(hit.point);
            }
        }

        if (clickedUnit)
        {
            Ray ray2 = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit2;

            if (Physics.Raycast(ray2, out hit2, 1000f, groundLayerMask))
            {
                mousePos = hit2.point;
                toPlace.transform.position = mousePos;
            }
        }
        if (Input.GetMouseButtonDown(0) && toPlace != null)
        {
            UnitValidate validator = toPlace.GetComponentInChildren<UnitValidate>();
            //Unit unit = toPlace.GetComponent<Unit>();
            preparingUnit = false;
            if (validator != null && validator.hasValidPlacement)
            {
                validator.SetPlacementMode(PlacementMode.Fixed);
                toPlace.transform.position += Vector3.up * offset.y;
                OnPlacedUnit?.Invoke();
                isPlacingUnit = false;
                validator.setUnitOnGround = true;
                foreach (NavMeshAgent agent in validator.GetComponentsInChildren<NavMeshAgent>())
                {
                    agent.enabled = true;
                }
                toPlace = null;
                unitPrefab = null;

            }
            else
            {
                Destroy(toPlace);
                isPlacingUnit = false;
                toPlace = null;
                unitPrefab = null;
            }
        }
    }
    private void RemoveUnitCheck()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hitAfterPlace;
            Ray rayAfterPlace = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayAfterPlace, out hitAfterPlace))
            {
                if (hitAfterPlace.collider.CompareTag("Unit") && hitAfterPlace.collider.GetComponent<UnitValidate>().setUnitOnGround)
                {
                    Debug.Log("unit");
                   // currentSelectedUnit = hitAfterPlace.collider.gameObject.GetComponent<Unit>();
                }
            }
        }
       /* if (Input.GetKeyDown(KeyCode.Delete) && currentSelectedUnit != null)
        {
            OnPlacedUnit?.Invoke(-1, -currentSelectedUnit.GetComponent<Unit>().unitSO.commandPoints);
            Destroy(currentSelectedUnit.gameObject);
            isPlacingUnit = false;
            toPlace = null;
            unitPrefab = null;
            currentSelectedUnit = null;
        }*/
    }

    private void _PrepareUnit(Vector3 _position)
    {
        toPlace = Instantiate(unitPrefab, _position, Quaternion.identity);
        preparingUnit = true;
        UnitValidate validator = toPlace.GetComponent<UnitValidate>();
        if (validator != null)
        {
            validator.hasValidPlacement = true; 
            validator.SetPlacementMode(PlacementMode.Valid); 
        }
    }
}
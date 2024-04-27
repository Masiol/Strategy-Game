using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FormationSelector : MonoBehaviour
{
    public FormationArmy selectedFormationArmy;
    public LayerMask layer;

    private void Start()
    {
        UnitPlacer.OnPlacedUnit += ResetSelectedFormation;
        UnitPlacer.OnPreparedPlaceUnit += ResetSelectedFormation;
       // layer = ~(1 << 5);
    }

    private void UnitPlacer_OnPreparedPlaceUnit()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {   
       if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null)
            {
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                pointerEventData.position = Input.mousePosition;

                var raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (var result in raycastResults)
                    {
                        if (result.gameObject.CompareTag("UIFormation") || result.gameObject.CompareTag("UIUnit"))
                        {
                            return;
                        }
                      /*  else if(result.gameObject.CompareTag("Untagged"))
                        {
                            if (selectedFormationArmy != null)
                            {
                                selectedFormationArmy.SelectFormation(false);
                                selectedFormationArmy.GetComponent<SelectedFormationOptions>().HideButtons();
                                selectedFormationArmy = null;
                                FindObjectOfType<FormationUIController>().HideFormationUI();
                            }
                        }*/
                    }
                }
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (hit.collider.CompareTag("Formation"))
                {
                    FormationArmy formation = hit.collider.GetComponent<FormationArmy>();
                    if (formation != null)
                    {
                        formation.Clicked++;
                        if (selectedFormationArmy != null && selectedFormationArmy != formation)
                        {
                            ResetSelectedFormation();

                        }

                        if (formation.GetFirstClicked() > 1)
                        {
                            selectedFormationArmy = formation;
                            selectedFormationArmy.GetComponent<SelectedFormationOptions>().ShowButtons();
                            formation.SelectFormation(true);
                        }
                    }
                }
                else if (hit.collider.CompareTag("UIUnit"))
                {
                    Debug.Log("Clicked on UIUnit formation");
                    return;
                }
                else
                {
                    ResetSelectedFormation();
                }
            }
            else if (EventSystem.current.IsPointerOverGameObject())
            {
                GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

                if (clickedObject != null && clickedObject.CompareTag("UIFormation"))
                {
                    Debug.Log("Clicked on UI formation");
                    return; 
                }
            }
            else
            {
                ResetSelectedFormation();
            }
        }
    }
    public void DeselectAndDestroyFormation()
    {
        if (selectedFormationArmy != null)
        {
            selectedFormationArmy.SelectFormation(false);      
            FindObjectOfType<FormationUIController>().HideFormationUI();
            Destroy(selectedFormationArmy.gameObject);
            selectedFormationArmy = null;
        }
    }

    private void ResetSelectedFormation()
    {
        if (selectedFormationArmy != null)
        {
            selectedFormationArmy.SelectFormation(false);
            selectedFormationArmy.GetComponent<SelectedFormationOptions>().HideButtons();
            selectedFormationArmy = null;
            FindObjectOfType<FormationUIController>().HideFormationUI();
        }
    }
}

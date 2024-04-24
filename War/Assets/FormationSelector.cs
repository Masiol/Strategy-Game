using UnityEngine;
using UnityEngine.EventSystems;

public class FormationSelector : MonoBehaviour
{
    public FormationArmy selectedFormationArmy;
    public LayerMask layer;

    private void Start()
    {
        layer = ~(1 << 5);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (hit.collider.CompareTag("Formation"))
                {
                    FormationArmy formation = hit.collider.GetComponent<FormationArmy>();
                    if (formation != null)
                    {
                        if (selectedFormationArmy != null && selectedFormationArmy != formation)
                        {
                            selectedFormationArmy.SelectFormation(false);
                        }

                        selectedFormationArmy = formation;
                        formation.SelectFormation(true);
                    }
                }
                else
                {
                    if (selectedFormationArmy != null)
                    {
                        selectedFormationArmy.SelectFormation(false);
                        selectedFormationArmy = null;
                    }
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
                if (selectedFormationArmy != null)
                {
                    selectedFormationArmy.SelectFormation(false);
                    selectedFormationArmy = null;
                }
            }
        }
    }
}

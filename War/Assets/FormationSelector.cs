using UnityEngine;
using UnityEngine.EventSystems;

public class FormationSelector : MonoBehaviour
{
    private SquadManager selectedSquad;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the click was on a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // If the click is on a UI element, do not proceed with selection or deselection
                return;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Formation"))
                {
                    // Check if the hit object is a new squad
                    SquadManager squad = hit.transform.GetComponent<SquadManager>();
                    if (squad != null)
                    {
                        SelectSquad(squad);
                    }
                }
                else
                {
                    DeselectSquad();
                }
            }
            else
            {
                DeselectSquad();
            }
        }
    }

    private void SelectSquad(SquadManager squad)
    {
        selectedSquad = squad;
        GameEvents.SelectSquad(squad);
        Debug.Log("Squad Selected: " + squad.name);
    }

    private void DeselectSquad()
    {
        if (selectedSquad != null)
        {
            Debug.Log("Squad Deselected: " + selectedSquad.name);
            selectedSquad = null;
            GameEvents.DeselectSquad();
        }
    }
}

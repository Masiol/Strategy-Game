using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationControler : MonoBehaviour
{
    private FormationSelector formationSelector;

    private void Start()
    {
        formationSelector = FindObjectOfType<FormationSelector>();
    }

    public void SetFormation(FormationBase _formationBase)
    {
        if (formationSelector.selectedFormationArmy != null)
        {
            FormationSettings formationSettings = formationSelector.selectedFormationArmy.GetComponent<FormationSettings>();
            if(formationSettings != null)
            {
                Debug.Log("tutaj git");
            }
            formationSelector.selectedFormationArmy.ChangeFormation(_formationBase, formationSettings);
        }
    }
}

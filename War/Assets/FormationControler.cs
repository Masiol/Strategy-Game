using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationControler : MonoBehaviour
{
    //[SerializeField] private List<FormationBase> formations = new List<FormationBase>();
    private FormationSelector formationSelector;

    private void Start()
    {
        formationSelector = FindObjectOfType<FormationSelector>();
    }

    public void SetFormation(FormationBase _formationBase)
    {
        if (formationSelector.selectedFormationArmy != null)
            formationSelector.selectedFormationArmy.ChangeFormation(_formationBase);
    }
}

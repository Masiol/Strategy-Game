using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitData : MonoBehaviour
{
    public UnitBase unit;
    public GameObject formation;

    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        formation = Resources.Load<GameObject>("ScriptableObjects/UnitFormation/" + unit.race.name + "/" + unit.race.name + unit.type + "Formation");
        btn.onClick.AddListener(SetPrefab);
    }

    private void SetPrefab()
    {
        UnitPlacer unitPlacer = FindObjectOfType<UnitPlacer>();
        if (unitPlacer != null)
        {
            unitPlacer.SetUnitPrefab(formation);
        }
    }
}

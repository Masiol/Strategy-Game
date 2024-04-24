using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform unitElement;
    [SerializeField] private RectTransform parent;

    public void LoadUnits(string _raceName)
    {
        UnitBase[] units = Resources.LoadAll<UnitBase>("ScriptableObjects/Race/" + _raceName + "/Units");

        for (int i = 0; i < units.Length; i++)
        {
            var go = Instantiate(unitElement);
            go.SetParent(parent);
            go.GetComponent<UnitData>().unit = units[i];

        }
    }
}

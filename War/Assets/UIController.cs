using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform racePanel;
    [SerializeField] private RectTransform unitsPanel;
    [SerializeField] private RectTransform startButton;
    [SerializeField] private RectTransform unitStats;
    [SerializeField] private RectTransform raceBonus;
    [SerializeField] private RectTransform formationBonus;

    [SerializeField] private Button[] raceUnits;

    [SerializeField] private UnitUIManager unitUIManager;

    private void Start()
    {
        unitUIManager = FindObjectOfType<UnitUIManager>();
    }

    public void ShowUnitsPanel()
    {
        unitUIManager.ShowUIUnitPanel();
    }
    public void HideUnitsPanel()
    {
        unitUIManager.HideUIUnitPanel();
    }
}

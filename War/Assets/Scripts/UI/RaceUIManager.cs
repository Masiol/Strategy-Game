using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class RaceUIManager : MonoBehaviour
{
    [SerializeField] private Button humanButton;
    [SerializeField] private Button elfButton;
    [SerializeField] private Button orcButton;
    [SerializeField] private Button undeadButton;
    [SerializeField] private Transform unitsParent;
    [SerializeField] private Button unitButtonPrefab;

    private ObjectPlacer objectPlacer;

    void Start()
    {
        objectPlacer = FindObjectOfType<ObjectPlacer>();

        humanButton.onClick.AddListener(() => LoadUnits("Human"));
        elfButton.onClick.AddListener(() => LoadUnits("Elves"));
        orcButton.onClick.AddListener(() => LoadUnits("Orcs"));
        undeadButton.onClick.AddListener(() => LoadUnits("Undead"));
    }

    private void LoadUnits(string race)
    {
        var units = Resources.LoadAll<UnitBase>("ScriptableObjects/"+race+"Units").ToList();
        ClearUnitsDisplay();

        foreach (var unit in units)
        {
            var unitUI = Instantiate(unitButtonPrefab);
            unitUI.name = $"{unit.unitName}";
            unitUI.transform.SetParent(unitsParent, false);
            unitUI.GetComponent<Button>().onClick.AddListener(() => objectPlacer.SpawnUnit(unit.unitFormation));

        }

        Debug.Log($"Loaded {units.Count} units for race: {race}");
    }

    private void ClearUnitsDisplay()
    {
        foreach (Transform child in unitsParent)
        {
            Destroy(child.gameObject);
        }
    }
}

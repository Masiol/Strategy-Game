using System.Collections.Generic;
using UnityEngine;

public class FormCreator : MonoBehaviour
{
    public UnitFormationConfig config;
    [HideInInspector] public List<GameObject> units = new List<GameObject>();

    [SerializeField] private Transform spawnPoint;

    private FormationArmy formationArmy;
    private BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }
        SpawnUnits();
        ConfigureCollider();
        formationArmy = GetComponent<FormationArmy>();
        formationArmy.FormationCompleted += OnFormationCompleted;
    }

    private void SpawnUnits()
    {
        Transform unitPlace = spawnPoint;
        int rows = Mathf.CeilToInt((float)config.totalUnits / config.unitsPerRow);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < config.unitsPerRow; col++)
            {
                int index = row * config.unitsPerRow + col;
                if (index >= config.totalUnits) break;

                Vector3 position = new Vector3(col * config.spacing, 0, row * config.spacing) + unitPlace.position;
                var go = Instantiate(config.unitPrefab, position, Quaternion.identity, unitPlace);
                go.name = "Unit" + col;
                units.Add(go);
            }
        }
    }

    private void Update()
    {
        ConfigureCollider();
    }

    private void OnFormationCompleted()
    {
        ConfigureCollider();
    }

    public void ConfigureCollider()
    {
        Vector3 minBounds = Vector3.one * Mathf.Infinity;
        Vector3 maxBounds = Vector3.one * Mathf.NegativeInfinity;

        foreach (GameObject unit in units)
        {
            Vector3 unitPosition = unit.transform.position;
            minBounds = Vector3.Min(minBounds, unitPosition);
            maxBounds = Vector3.Max(maxBounds, unitPosition);
        }

        Vector3 center = (minBounds + maxBounds) * 0.5f;
        Vector3 size = maxBounds - minBounds;

        float customHeight = 5.0f;
        size.y = customHeight;

        float margin = 0.1f;
        size += Vector3.one * margin;

        boxCollider.size = size;
        boxCollider.center = center - transform.position;

        // Call the child collider configuration
        ConfigureChildCollider(size, center);
    }

    private void ConfigureChildCollider(Vector3 parentSize, Vector3 parentCenter)
    {
        GameObject childObject = transform.GetChild(0).gameObject;
        BoxCollider childCollider = childObject.GetComponent<BoxCollider>();
        if (childCollider == null)
        {
            childCollider = childObject.AddComponent<BoxCollider>();
        }

        // Calculate expanded size and center adjustment
        float expansion = 4.0f;
        Vector3 expandedSize = parentSize + Vector3.one * expansion;
        Vector3 expandedCenter = parentCenter;  // Center remains the same, only size changes

        childCollider.size = expandedSize;
        childCollider.center = expandedCenter - transform.position;
        childCollider.isTrigger = true;
    }
}

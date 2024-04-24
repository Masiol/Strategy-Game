
using UnityEngine;

[CreateAssetMenu(fileName = "UnitFormationConfig", menuName = "ScriptableObjects/UnitFormationConfig", order = 1)]
public class UnitFormationConfig : ScriptableObject
{
    public GameObject unitPrefab; // Prefab jednostki
    public int totalUnits = 10; // Ca³kowita liczba jednostek do zespawnienia
    public int unitsPerRow = 5; // Liczba jednostek w rzêdzie
    public float spacing = 1.5f; // Odstêp miêdzy jednostkami
}
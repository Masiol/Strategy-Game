
using UnityEngine;

[CreateAssetMenu(fileName = "UnitFormationConfig", menuName = "ScriptableObjects/UnitFormationConfig", order = 1)]
public class UnitFormationConfig : ScriptableObject
{
    public GameObject unitPrefab; // Prefab jednostki
    public int totalUnits = 10; // Ca�kowita liczba jednostek do zespawnienia
    public int unitsPerRow = 5; // Liczba jednostek w rz�dzie
    public float spacing = 1.5f; // Odst�p mi�dzy jednostkami
}
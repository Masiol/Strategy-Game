using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Squad", menuName = "Army/Squad")]
public class UnitSquad : ScriptableObject
{
    public GameObject unitPrefab;
    public int unitCount;
    public float spacing;
    public Formation formation;
}

public enum Formation
{
    Square,
    Circle,
    Triangle,
    Line
}

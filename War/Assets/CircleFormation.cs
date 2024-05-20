using UnityEngine;

public class CircleFormation : IFormationStrategy
{
    public void ArrangeUnits(GameObject _unitPrefab, int _unitCount, Transform _squadTransform, float _spacing)
    {
        ClearExistingUnits(_squadTransform);
        CreateUnits(_unitPrefab, _unitCount, _squadTransform, _spacing);
    }

    public void ReArrangeUnits(Transform _squadTransform, float _spacing)
    {
        int unitCount = _squadTransform.childCount;
        float angleStep = 360f / unitCount;
        float radius = _spacing;

        for (int i = 0; i < unitCount; i++)
        {
            Transform unit = _squadTransform.GetChild(i);
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            unit.position = _squadTransform.position + position;  // Position adjusted relative to the squad center
        }
    }

    private void CreateUnits(GameObject _unitPrefab, int _unitCount, Transform _squadTransform, float _spacing)
    {
        float angleStep = 360f / _unitCount;
        float radius = _spacing;

        for (int i = 0; i < _unitCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            GameObject.Instantiate(_unitPrefab, _squadTransform.position + position, Quaternion.identity, _squadTransform);
        }
    }

    private void ClearExistingUnits(Transform squadTransform)
    {
        foreach (Transform child in squadTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
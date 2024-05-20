using UnityEngine;

public class LineFormation : IFormationStrategy
{
    public void ArrangeUnits(GameObject _unitPrefab, int _unitCount, Transform _squadTransform, float _spacing)
    {
        ClearExistingUnits(_squadTransform);
        CreateUnits(_unitPrefab, _unitCount, _squadTransform, _spacing);
    }

    public void ReArrangeUnits(Transform _squadTransform, float _spacing)
    {
        int unitCount = _squadTransform.childCount;
        float offset = _spacing;
        Vector3 startPosition = _squadTransform.position - Vector3.right * (offset * (unitCount - 1) / 2);

        for (int i = 0; i < unitCount; i++)
        {
            Transform unit = _squadTransform.GetChild(i);
            Vector3 position = startPosition + Vector3.right * offset * i;
            unit.position = position;
        }
    }

    private void CreateUnits(GameObject _unitPrefab, int _unitCount, Transform _squadTransform, float _spacing)
    {
        Vector3 startPosition = -Vector3.right * (_spacing * (_unitCount - 1) / 2);

        for (int i = 0; i < _unitCount; i++)
        {
            Vector3 position = startPosition + Vector3.right * _spacing * i;
            GameObject.Instantiate(_unitPrefab, _squadTransform.position + position, Quaternion.identity, _squadTransform);
        }
    }

    private void ClearExistingUnits(Transform _squadTransform)
    {
        foreach (Transform child in _squadTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}

using UnityEngine;

public interface IFormationStrategy
{
    void ArrangeUnits(GameObject _unitPrefab, int _unitCount, Transform _squadTransform, float _spacing);
    void ReArrangeUnits(Transform _squadTransform, float _spacing);
}
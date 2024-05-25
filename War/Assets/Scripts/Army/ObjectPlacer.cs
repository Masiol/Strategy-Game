using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class ObjectPlacer : MonoBehaviour
{
    private GameObject currentObject; 
    public List<Transform> spawnPoints;
    private int currentSpawnIndex = 0;

    public void SpawnUnit(GameObject _currentObject)
    {
        if (currentSpawnIndex >= spawnPoints.Count)
        {
            Debug.LogError("No more spawn points available!");
            return;
        }

        Transform spawnPoint = spawnPoints[currentSpawnIndex];
        var go = Instantiate(_currentObject, spawnPoint.position, Quaternion.identity, spawnPoint);
        currentSpawnIndex = (currentSpawnIndex + 1) % spawnPoints.Count;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Gizmos.DrawSphere(spawnPoints[i].transform.position, 1f);
        }
    }
}

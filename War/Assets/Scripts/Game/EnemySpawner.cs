using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct SquadSpawnInfo
{
    public GameObject unitSquadPrefab;
    public Formation formation;
}
public class EnemySpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<SquadSpawnInfo> squadSpawnInfos;
    private int currentSpawnIndex = 0;


    public void SpawnUnits()
    {

        for (int i = 0; i < Mathf.Min(spawnPoints.Count, squadSpawnInfos.Count); i++)
        {
            SpawnUnitAtIndex(i);
        }
    }

    public void SpawnUnitAtIndex(int index)
    {
        if (index >= 0 && index < Mathf.Min(spawnPoints.Count, squadSpawnInfos.Count))
        {
            Transform spawnPoint = spawnPoints[index];
            SquadSpawnInfo spawnInfo = squadSpawnInfos[index];
            GameObject squadPrefab = spawnInfo.unitSquadPrefab;
            Formation formation = spawnInfo.formation;

            if (spawnPoint != null && squadPrefab != null)
            {
                // Szukaj pierwszego wolnego rodzica
                Transform parent = null;
                foreach (Transform point in spawnPoints)
                {
                    if (point.childCount == 0)
                    {
                        parent = point;
                        break;
                    }
                }

                if (parent != null)
                {
                    // Spawnuj jednostkê jako dziecko wybranego rodzica
                    GameObject squadObj = Instantiate(squadPrefab, parent.position, Quaternion.identity, parent);
                    squadObj.GetComponent<SquadManager>().shouldBeEnemy = true;
                }
                else
                {
                    Debug.LogError("No available spawn point found!");
                }
            }
            else
            {
                Debug.LogError("Spawn point or squad prefab is not assigned!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Gizmos.DrawSphere(spawnPoints[i].transform.position, 1f);
        }
    }
}

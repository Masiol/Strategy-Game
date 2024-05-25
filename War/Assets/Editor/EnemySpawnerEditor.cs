using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemySpawner spawner = (EnemySpawner)target;

        GUILayout.Space(10);

        EditorGUILayout.LabelField("Spawn Units", EditorStyles.boldLabel);

        for (int i = 0; i < spawner.squadSpawnInfos.Count; i++)
        {
            SquadSpawnInfo spawnInfo = spawner.squadSpawnInfos[i];

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Spawn Point " + i.ToString(), GUILayout.Width(100));

            spawnInfo.unitSquadPrefab = EditorGUILayout.ObjectField(spawnInfo.unitSquadPrefab, typeof(GameObject), false) as GameObject;
            spawnInfo.formation = (Formation)EditorGUILayout.EnumPopup(spawnInfo.formation);

            if (GUILayout.Button("Spawn", GUILayout.Width(60)))
            {
                spawner.SpawnUnitAtIndex(i);
            }

            spawner.squadSpawnInfos[i] = spawnInfo;

            EditorGUILayout.EndHorizontal();
        }
    }
}

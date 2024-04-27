using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitController))]
public class UnitControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UnitController unitController = (UnitController)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Attack"))
        {
            unitController.CustomAttack();
        }

        if (GUILayout.Button("Run"))
        {
            unitController.CustomRun();
        }

        if (GUILayout.Button("Idle"))
        {
            unitController.CustomIdle();
        }
    }
}

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using static GhostCollisionChecker;

[CustomEditor(typeof(GhostCollisionChecker))]
public class ScriptsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();  // Draws the default inspector

        GhostCollisionChecker checker = (GhostCollisionChecker)target;

        if (GUILayout.Button("Load MeshRenderers"))
        {
            LoadMeshRenderers(checker);
        }

        if (GUILayout.Button("Apply Ghost Material"))
        {
            checker.ApplyGhostMaterial();
        }

        if (GUILayout.Button("Apply Wrong Material"))
        {
            checker.ApplyWrongMaterial();
        }
        if (GUILayout.Button("Apply Main Materials"))
        {
            checker.ApplyMainMaterials();
        }
    }

    private void LoadMeshRenderers(GhostCollisionChecker checker)
    {
        // Clear previous entries if any
        checker.rendererMaterials.Clear();

        // Get all MeshRenderer components from the attached GameObject and its children
        MeshRenderer[] renderers = checker.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            RendererMaterials rm = new RendererMaterials
            {
                renderer = renderer,
                originalMaterials = renderer.sharedMaterials // Use sharedMaterials to avoid asset duplication
            };
            checker.rendererMaterials.Add(rm);
        }

        // Mark the checker as dirty to ensure changes are saved
        EditorUtility.SetDirty(checker);
    }
}

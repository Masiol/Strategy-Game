using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacementMode
{
    Fixed,
    Valid,
    Invalid
}

public class UnitValidate : MonoBehaviour
{
    public Material validPlacementMaterial;
    public Material invalidPlacementMaterial;
    public SkinnedMeshRenderer[] meshComponents;

    [HideInInspector] public bool hasValidPlacement;
    [HideInInspector] public bool isFixed;

    private Dictionary<SkinnedMeshRenderer, List<Material>> initialMaterials;
    private int _nObstacles;

    public bool setUnitOnGround;

    private void Awake()
    {
        meshComponents = GetComponentsInChildren<SkinnedMeshRenderer>();

        hasValidPlacement = true;
        isFixed = true;
        _nObstacles = 0;

        _InitializeMaterials();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _InitializeMaterials();
    }
#endif

    private void _InitializeMaterials()
    {
        if (initialMaterials == null)
            initialMaterials = new Dictionary<SkinnedMeshRenderer, List<Material>>();
        if (initialMaterials.Count > 0)
        {
            foreach (var l in initialMaterials) l.Value.Clear();
            initialMaterials.Clear();
        }

        foreach (SkinnedMeshRenderer r in meshComponents)
        {
            initialMaterials[r] = new List<Material>(r.sharedMaterials);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!setUnitOnGround)
        {
            if (other.tag == "Obstacle" || other.tag == "SolidFormation" || other.tag == "EnemySide")
            {
                _nObstacles++;
                SetPlacementMode(PlacementMode.Invalid);
            }
            if (isFixed) return;

            if (_IsGround(other.gameObject)) return;

            _nObstacles++;
            SetPlacementMode(PlacementMode.Invalid);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!setUnitOnGround)
        {
            if (other.tag == "Obstacle" || other.tag == "SolidFormation" || other.tag == "EnemySide")
            {
                _nObstacles--;
                if (_nObstacles == 0)
                    SetPlacementMode(PlacementMode.Valid);
            }
            if (isFixed) return;

            if (_IsGround(other.gameObject)) return;

            _nObstacles--;
            if (_nObstacles == 0)
                SetPlacementMode(PlacementMode.Valid);
        }
    }

    public void SetPlacementMode(PlacementMode _mode)
    {
        if (_mode == PlacementMode.Fixed)
        {
            isFixed = true;
            hasValidPlacement = true;
        }
        else if (_mode == PlacementMode.Valid)
        {
            hasValidPlacement = true;
        }
        else
        {
            hasValidPlacement = false;
        }
        SetMaterial(_mode);
    }

    public void SetMaterial(PlacementMode _mode)
    {
        if (_mode == PlacementMode.Fixed)
        {
            foreach (SkinnedMeshRenderer r in meshComponents)
                r.sharedMaterials = initialMaterials[r].ToArray();
        }
        else
        {
            Material matToApply = _mode == PlacementMode.Valid
                ? validPlacementMaterial : invalidPlacementMaterial;

            Material[] m; int nMaterials;
            foreach (SkinnedMeshRenderer r in meshComponents)
            {
                nMaterials = initialMaterials[r].Count;
                m = new Material[nMaterials];
                for (int i = 0; i < nMaterials; i++)
                    m[i] = matToApply;
                r.sharedMaterials = m;
            }
        }
    }

    private bool _IsGround(GameObject _o)
    {
        return ((1 << _o.layer) & UnitPlacer.instance.groundLayerMask.value) != 0;
    }
}
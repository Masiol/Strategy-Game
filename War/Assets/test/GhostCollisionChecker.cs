using UnityEngine;
using System.Collections.Generic;

public class GhostCollisionChecker : MonoBehaviour
{
    public Material ghostMaterial;
    public Material wrongMaterial;
    private bool isColliding = false;
    public List<RendererMaterials> rendererMaterials;

    public bool noLoadOnStart;
    public float additionalYPos;

    public bool isPlacedFromStart;
    public bool isPlaced;

    [System.Serializable]
    public class RendererMaterials
    {
        public MeshRenderer renderer;
        public Material[] originalMaterials;
    }

    void Start()
    {
        if (isPlacedFromStart)
            isPlaced = true;

        LoadRendererMaterials();
        if(!noLoadOnStart)
        ApplyGhostMaterial();
        SetCollidersTrigger(true);
    }

    void LoadRendererMaterials()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        rendererMaterials = new List<RendererMaterials>();

        foreach (MeshRenderer renderer in renderers)
        {
            rendererMaterials.Add(new RendererMaterials
            {
                renderer = renderer,
                originalMaterials = renderer.materials  
            });
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpecialTag") || other.gameObject.CompareTag("Furniture"))
        {
            isColliding = true;
            ApplyWrongMaterial(); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SpecialTag") || other.gameObject.CompareTag("Furniture"))
        {
            isColliding = false;
            ApplyGhostMaterial();  
        }
    }

    public void ApplyGhostMaterial()
    {
        if (!isPlaced)
        {
            foreach (var rm in rendererMaterials)
            {
                Material[] ghostMaterials = new Material[rm.renderer.materials.Length];
                for (int i = 0; i < ghostMaterials.Length; i++)
                {
                    ghostMaterials[i] = ghostMaterial;
                }
                rm.renderer.materials = ghostMaterials;
            }
        }
    }

    public void ApplyWrongMaterial()
    {
        if (!isPlaced)
        {
            foreach (var rm in rendererMaterials)
            {
                Material[] wrongMaterials = new Material[rm.renderer.materials.Length];
                for (int i = 0; i < wrongMaterials.Length; i++)
                {
                    wrongMaterials[i] = wrongMaterial;
                }
                rm.renderer.materials = wrongMaterials;
            }
        }
    }

    public void ApplyMainMaterials()
    {
        Debug.Log("main");
        foreach (var rm in rendererMaterials)
        {
            rm.renderer.materials = rm.originalMaterials;
        }
        isPlaced = true;
        SetCollidersTrigger(false); 
    }

    public bool IsColliding()
    {
        return isColliding;
    }

    private void SetCollidersTrigger(bool isTrigger)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var col in colliders)
        {
            col.isTrigger = isTrigger;
        }
    }
}

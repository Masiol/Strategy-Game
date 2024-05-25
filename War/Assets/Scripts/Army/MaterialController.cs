using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public Material validMaterial;
    public Material invalidMaterial;
    public Material finalMaterial;

    public void SetValid()
    {
        SetMaterial(validMaterial);
    }

    public void SetInvalid()
    {
        SetMaterial(invalidMaterial);
    }

    public void SetFinalMaterial()
    {
        SetMaterial(finalMaterial);
    }

    private void SetMaterial(Material _material)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = _material;
        }
    }
}

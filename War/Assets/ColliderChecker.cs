using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    private MaterialController materialController;
    private FormationDragger formationDragger;
    [SerializeField] private BoxCollider boxCollider;

    private void Start()
    {
        materialController = GetComponent<MaterialController>();
        formationDragger = FindObjectOfType<FormationDragger>();
        UpdateColliderBounds(); 
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (GetComponent<SquadManager>().isGrounded)
            return;

        if (_other.CompareTag("Army") || _other.CompareTag("Obstacle") || _other.CompareTag("EnemySide"))
        {
            materialController.SetInvalid();
            formationDragger.SetState(PlacementState.Invalid);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (GetComponent<SquadManager>().isGrounded)
            return;

            if (_other.CompareTag("Army") || _other.CompareTag("Obstacle") || _other.CompareTag("EnemySide"))
            {
                materialController.SetValid();
                formationDragger.SetState(PlacementState.Valid);
            }
    }
    public void UpdateColliderBounds()
    {
        Bounds bounds = new Bounds(transform.position, Vector3.zero);

        foreach (Transform child in transform)
        {
            bounds.Encapsulate(child.position);
        }

        boxCollider.size = Vector3.zero;

        boxCollider.center = bounds.center - transform.position + Vector3.up * 1.5f;
        float extension = 1.0f;
        boxCollider.size = bounds.size + new Vector3(extension, extension, extension) * 2f + Vector3.up * 1.5f;
    }
}
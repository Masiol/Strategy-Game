using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMapBounds : MonoBehaviour
{
    private BoxCollider boxCollider;
    public Vector3 LastDisplacement { get; private set; } = Vector3.zero;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
     /*   if (other.CompareTag("MapBounds"))
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 hitDirection = transform.position - contactPoint;
            hitDirection = hitDirection.normalized;

            float moveDistance = 5; // przyk³adowa wartoœæ, mo¿na to dostosowaæ
            Vector3 displacement = hitDirection * moveDistance;

            boxCollider.center += displacement;
            LastDisplacement += displacement;
        }*/
    }
}

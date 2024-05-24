using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    public Camera cam;
    public PlacementObject[] objectsToPlace;

    private GameObject currentGhost;
    private GameObject currentSelectedObject;
    private int currentObjectIndex = 0;
    public LayerMask layer;
    public float maxDistance;
    private float currentY;
    public float rotationSpeed = 90.0f;
    private GhostCollisionChecker collisionChecker;
    private GhostCollisionChecker previousChecker;

    public KeyCode rotateLeft;
    public KeyCode rotateRight;
    public KeyCode pickupObject;
    public Vector3 offset;
    private bool isDraggingObject = false;
    private Vector3 initialObjectPosition;

    [Serializable]
    public class PlacementObject
    {
        public GameObject prefab;
        public PlacementType placementType;
        public float yPos;
    }

    public enum PlacementType
    {
        Horizontal,
        Vertical
    }

    void Update()
    {
        HandleCameraMovement();
        HandleObjectPlacement();
        LockGhostXRotation();
        DrawRaycast();
        if (Input.GetKeyDown(pickupObject))
        {
            HandleObjectSelection();
        }

        UpdateSelectedObjectPosition();
    }

    void LockGhostXRotation()
    {
        if (currentGhost != null)
        {
            var rotation = currentGhost.transform.eulerAngles;
            currentGhost.transform.eulerAngles = new Vector3(0, rotation.y, 0);
        }
    }

    void HandleCameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation = cam.transform.eulerAngles;
        rotation.x -= mouseY;
        rotation.y += mouseX;
        cam.transform.eulerAngles = rotation;
    }

    void HandleObjectPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentObjectIndex = 0;
            currentY = objectsToPlace[0].yPos;
            UpdateGhostObject();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentObjectIndex = 1;
            currentY = objectsToPlace[1].yPos;
            UpdateGhostObject();
        }

        if (currentGhost != null)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, ~layer))
            {
                Vector3 position = hit.point;
                if (objectsToPlace[currentObjectIndex].placementType == PlacementType.Vertical)
                {
                    position.y = currentGhost.transform.position.y;
                }
                else if (objectsToPlace[currentObjectIndex].placementType == PlacementType.Horizontal)
                {
                    position.y = currentY;
                }
                currentGhost.transform.position = position;
            }

            if (Input.GetKey(rotateRight))
            {
                currentGhost.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(rotateLeft))
            {
                currentGhost.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && !currentGhost.GetComponent<GhostCollisionChecker>().IsColliding())
            {
                var go = Instantiate(objectsToPlace[currentObjectIndex].prefab, currentGhost.transform.position, currentGhost.transform.rotation);
                previousChecker = go.GetComponent<GhostCollisionChecker>();
                Invoke("test", 0.02f);
                Destroy(currentGhost);
            }
        }
    }

    private void test()
    {
        previousChecker.GetComponent<GhostCollisionChecker>().ApplyMainMaterials();
        previousChecker = null;
    }

    void UpdateGhostObject()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }
        currentGhost = Instantiate(objectsToPlace[currentObjectIndex].prefab, Vector3.zero, Quaternion.identity);
        currentGhost.transform.SetParent(cam.transform);
        collisionChecker = currentGhost.GetComponent<GhostCollisionChecker>();

        if (objectsToPlace[currentObjectIndex].placementType == PlacementType.Horizontal)
        {
            currentY = objectsToPlace[currentObjectIndex].yPos;
        }
    }

    void HandleObjectSelection()
    {
        if (currentGhost == null)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Furniture"))
                {
                    hit.collider.gameObject.GetComponent<GhostCollisionChecker>().ApplyGhostMaterial();

                    if (currentSelectedObject != hitObject)
                    {
                        if (currentSelectedObject != null)
                        {
                            hit.collider.gameObject.GetComponent<GhostCollisionChecker>().ApplyGhostMaterial();
                            currentSelectedObject.GetComponent<Collider>().isTrigger = true;
                            currentSelectedObject.GetComponent<GhostCollisionChecker>().ApplyGhostMaterial();
                        }
                        currentSelectedObject = hitObject;
                        Invoke("test2", 0.01f);
                        currentSelectedObject.GetComponent<GhostCollisionChecker>().ApplyGhostMaterial();

                        // Initialize dragging
                        isDraggingObject = true;
                        initialObjectPosition = currentSelectedObject.transform.position;

                    }
                }
                else if (currentSelectedObject != null)
                {
                    currentSelectedObject.GetComponent<GhostCollisionChecker>().ApplyGhostMaterial();
                    currentSelectedObject = null;
                    isDraggingObject = false;
                }
            }
            else if (currentSelectedObject != null)
            {
                currentSelectedObject.GetComponent<GhostCollisionChecker>().ApplyGhostMaterial();
                currentSelectedObject = null;
                isDraggingObject = false;
            }
        }
    }
    private void test2()
    {
        currentSelectedObject.GetComponent<GhostCollisionChecker>().ApplyGhostMaterial();
    }

    void UpdateSelectedObjectPosition()
    {
        if (currentSelectedObject != null && isDraggingObject)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, ~layer))
            {
                Vector3 position = hit.point;
                GhostCollisionChecker checker = currentSelectedObject.GetComponent<GhostCollisionChecker>();
                checker.GetComponent<Collider>().isTrigger = true;
                checker.isPlaced = false;
                if (checker != null && checker.rendererMaterials != null && checker.rendererMaterials.Count > 0)
                {
                    if (objectsToPlace[currentObjectIndex].placementType == PlacementType.Vertical)
                    {
                        position.y = checker.rendererMaterials[0].renderer.transform.position.y;
                    }
                    else if (objectsToPlace[currentObjectIndex].placementType == PlacementType.Horizontal)
                    {
                        position.y = checker.additionalYPos;
                    }
                    currentSelectedObject.transform.position = position;
                }

                if (Input.GetKey(rotateRight))
                {
                    currentSelectedObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                }
                if (Input.GetKey(rotateLeft))
                {
                    currentSelectedObject.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
                }

                if (Input.GetKeyDown(KeyCode.Space) && !currentSelectedObject.GetComponent<GhostCollisionChecker>().IsColliding())
                {
                    currentSelectedObject.GetComponent<GhostCollisionChecker>().ApplyMainMaterials();
                    currentSelectedObject = null;
                    isDraggingObject = false;
                }
            }
        }
    }
    void DrawRaycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
    }
}

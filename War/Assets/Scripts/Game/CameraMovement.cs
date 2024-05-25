using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public CameraConfig config;
    private Vector3 velocity = Vector3.zero;
    private bool cameraMove;

    private void OnEnable()
    {
        GameEvents.StartGame += CanCameraMove;
    }

    private void OnDisable()
    {
        GameEvents.StartGame -= CanCameraMove;
    }

    private void CanCameraMove()
    {
        cameraMove = true;
    }

    private void Update()
    {
        if (!cameraMove)
            return;

        Vector3 pos = transform.position;

        pos = HandleMovement(pos);

        HandleFieldOfView();

        transform.position = pos;
    }

    private Vector3 HandleMovement(Vector3 targetPosition)
    {
        Vector3 newPosition = targetPosition;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - config.panBorderThickness)
        {
            newPosition.z += config.panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= config.panBorderThickness)
        {
            newPosition.z -= config.panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - config.panBorderThickness)
        {
            newPosition.x += config.panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= config.panBorderThickness)
        {
            newPosition.x -= config.panSpeed * Time.deltaTime;
        }

        newPosition.x = Mathf.Clamp(newPosition.x, config.minX, config.maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, config.minZ, config.maxZ);

        return Vector3.SmoothDamp(targetPosition, newPosition, ref velocity, config.smoothTime);
    }

    private void HandleFieldOfView()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView -= scroll * config.scrollSpeed * 100f * Time.deltaTime;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, config.minFOV, config.maxFOV);
    }
}
[System.Serializable]
public struct CameraConfig
{
    public float panSpeed;
    public float panBorderThickness;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float scrollSpeed;
    public float minFOV;
    public float maxFOV;
    public float smoothTime;
}
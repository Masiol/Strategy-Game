using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthSystem healthSystem;
    public Image healthFillImage;
    public Camera mainCamera; // Reference to the main camera

    private void OnEnable()
    {
        healthSystem.OnHealthChanged += UpdateHealthBar;
        healthSystem.OnDeath += HandleDeath;

        // Initialize the main camera reference
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the main camera
        }
    }

    private void OnDisable()
    {
        healthSystem.OnHealthChanged -= UpdateHealthBar;
        healthSystem.OnDeath -= HandleDeath;
    }

    private void Start()
    {
        healthFillImage.fillAmount = 1f;
    }

    private void Update()
    {
        // Update health bar orientation to face the camera every frame
        if (mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }

    private void UpdateHealthBar(float currentHealth)
    {
        healthFillImage.fillAmount = currentHealth / healthSystem.maxHealth;
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
        Debug.Log("Unit died!");
    }
}

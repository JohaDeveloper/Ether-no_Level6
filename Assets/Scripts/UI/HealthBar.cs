using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider; // La imagen que representa el relleno de la barra
    [SerializeField] private HealthSystem healthSystem; // Referencia al sistema de salud del personaje

    private void Start()
    {
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged.AddListener(UpdateHealthBar); // Escuchar el evento de cambio de salud
            UpdateHealthBar(); // Sincronizar la barra de salud al inicio
        }
    }

    public void UpdateHealthBar()
    {
        if (healthSystem != null && healthSlider != null)
        {
            // Actualizar el relleno basado en el porcentaje de salud
            float fillValue = healthSystem.GetHealthPercentage();
            healthSlider.value = fillValue * 100;
        }
    }
}
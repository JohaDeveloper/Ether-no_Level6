using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wine : MonoBehaviour
{
    [SerializeField] float healthAmount = 40f; 

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que colisionamos tiene el componente HealthSystem
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.Heal(+healthAmount);

            Destroy(this.gameObject);
        }
    }
}

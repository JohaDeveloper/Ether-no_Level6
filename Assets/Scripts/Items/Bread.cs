using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread: MonoBehaviour
{
    [SerializeField] float healthAmount = 20f; 


    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que colisionamos tiene el componente HealthSystem
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            // Aumentar la salud del jugador
            healthSystem.Heal(+healthAmount); 

            Destroy(this.gameObject);
        }
    }
}

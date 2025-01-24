using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
public class HealthSystem : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject powerupVFX;
    [SerializeField] AudioSource DieAudio;
    [SerializeField] AudioSource PowerUpAudio;
 
    float dietime = 5.0f;
    bool isAlive = true;

    public UnityEvent OnHealthChanged;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (OnHealthChanged == null)
        {
            OnHealthChanged = new UnityEvent();
        }
    }
 
    public float GetHealthPercentage()
    {
        return health / maxHealth; // Retorna el porcentaje de vida
    }

    public void TakeDamage(float damageAmount)
    {
        if(!isAlive)
        {
            return;
        }
        health -= damageAmount;
        animator.SetTrigger("damage");
        OnHealthChanged.Invoke();

        if (health <= 0)
        {
            Die();
        }
    }
 
    public void Heal(float healAmount)
   {       
       
    animator.SetTrigger("PowerUp");
    PowerUpVFX(transform.position += Vector3.up);
    health += healAmount;
    PowerUpAudio.Play();
    OnHealthChanged.Invoke();
    health = Mathf.Clamp(health, 0, 100); // Limitar la salud máxima 

   }

    void Die()
    {
        isAlive = false;
        DieAudio.Play();
        animator.SetTrigger("Die");
        Destroy(this.gameObject,dietime);
    }

    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 1f);
 
    }

    public void PowerUpVFX(Vector3 powerupPosition)
    {
        GameObject powerup = Instantiate(powerupVFX, powerupPosition, Quaternion.identity);
        Destroy(powerup, 3f);
 
    }
    
}
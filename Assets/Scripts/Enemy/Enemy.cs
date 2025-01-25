using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 3;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject dieVFX;
   // [SerializeField] GameObject item;

    [SerializeField] AudioSource DieAudio;
    [SerializeField] AudioSource HitAudio;
 
    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1.2f;
    [SerializeField] float aggroRange = 10f;
 
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;
    float dietime = 4.0f;
    bool isAlive = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
 
    // Update is called once per frame
    void Update()
    {

        if(!isAlive)
        {
            return;
        }

        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
 
        if (player == null)
        {
            return;
        }
 
        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;
 
        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print(true);
            player = collision.gameObject;
        }
    }
 
    void Die()
    {   
        
        isAlive = false;
        animator.SetTrigger("die");
        DieAudio.Play();
        DieVFX(transform.position += Vector3.up);
        //Item(transform.position);
        Destroy(this.gameObject, dietime);
        
    }
 
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
        HitAudio.Play();
 
        if (health <= 0)
        {   
            
            Die();
        }
    }
    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }
 
    public void HitVFX(Vector3 hitPosition)
    {
        
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 1f);
    }

    public void DieVFX(Vector3 diePosition)
    {
        GameObject die = Instantiate(dieVFX, diePosition, Quaternion.identity);
        Destroy(die, 2f);
    }

   /* public void Item(Vector3 itemPosition)
    {
        GameObject iitem = Instantiate(item, itemPosition, Quaternion.identity);
        Destroy(item, 10f);
    }
   */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
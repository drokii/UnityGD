using UnityEngine;
using System.Collections;
using UnityEngine.AI;

 
public class Enemy : MonoBehaviour
{
    public int startingHealth = 100;            
    public int currentHealth;                   
    public float sinkSpeed = 2.5f;                            
    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip attackClip;
    Transform player;                           
    NavMeshAgent nav;               

    Animator anim;                              
    AudioSource enemyAudio;                    
    ParticleSystem hitParticles;                
    CapsuleCollider capsuleCollider;                
    bool isDead;                                
    bool isSinking;
    private bool isMoving;

    void Awake()
    {
        
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isSinking)
        {            
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        MoveTowardsPlayer();
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        
        if (isDead)            
            return;

        enemyAudio.clip = hitClip;
        enemyAudio.Play();
 
        currentHealth -= amount;
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        StartSinking();
    }


    private void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
    private void MoveTowardsPlayer()
    {
        if (currentHealth > 0) // plug playerHealth.currentHealth > 0 here later
        {
            isMoving = true;
            nav.SetDestination(player.position);
        }
        else
        {
            isMoving = false;
            nav.enabled = false;
        }
    }
}

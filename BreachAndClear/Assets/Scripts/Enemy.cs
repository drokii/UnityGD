using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 0.5f;
    public float effectTimer = 1f;
    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip attackClip;

    Transform player;
    NavMeshAgent nav;

    Animator anim;
    AudioSource enemyAudio;
    GameObject hitParticles;
    SphereCollider sphereCollider;
    BoxCollider boxCollider;

    bool isDead;
    bool isSinking;
    private bool isMoving;

    float attackTimer;
    private bool isAttacking;

    void Awake()
    {
        attackTimer = 3f;
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = transform.Find("Flare").gameObject; ;
        sphereCollider = GetComponent<SphereCollider>();
        boxCollider = GetComponent<BoxCollider>();
        currentHealth = startingHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<Player>().getHealth();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //if (isSinking)
        //{
        //    transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        //}

        MoveTowardsPlayer();
        if (isAttacking)
        {
            Attack();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            isAttacking = true;
            if (!isDead)
            {
                nav.SetDestination(gameObject.transform.position);
            }

            anim.SetBool("IsMoving", false);

        }
    }
    void OnTriggerExit(Collider other)
    {

        isAttacking = false;

        if (!isDead)
        {
            nav.isStopped = false;
        }

        anim.SetBool("IsMoving", true);

    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {

        if (isDead)
            return;

        enemyAudio.clip = hitClip;
        enemyAudio.Play();

        currentHealth -= amount;
        hitParticles.transform.position = hitPoint;
        hitParticles.GetComponent<ParticleSystem>().Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    

    void Attack()
    {
        attackTimer += Time.deltaTime;

        string attack;

        System.Random r = new System.Random();
        int f = r.Next(0, 2);
        if (f == 1)
        {
            attack = "LightAttack";
        }

        else
        {
            attack = "HeavyAttack";
        }
        if (attackTimer > 2f)
        {
            anim.SetTrigger(attack);
            attackTimer = 0f;
        }
    }
    void Death()
    {
        System.Random r = new System.Random();
        //GetComponent<Rigidbody>().isKinematic = true;
        isDead = true;
        nav.isStopped = true;       
        //boxCollider.isTrigger = true;

        int varyAnimation = r.Next(1, 3);
        anim.SetTrigger("Dead" + Convert.ToString(varyAnimation));

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        Destroy(gameObject, 2f);
        //StartSinking();
    }


    private void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
    private void MoveTowardsPlayer()
    {
        if (currentHealth > 0) // plug playerHealth.currentHealth > 0 here later
        {
            GetComponent<MoveTowardsTarget>().enabled = true;
            //isMoving = true;
            // nav.SetDestination(player.position);
        }
        else
        {
            GetComponent<MoveTowardsTarget>().enabled = false;

            //isMoving = false;
            //nav.Stop();
        }
    }
}

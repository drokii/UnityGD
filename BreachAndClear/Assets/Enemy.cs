using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

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
    SphereCollider sphereCollider;

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
        hitParticles = GetComponent<ParticleSystem>();
        sphereCollider = GetComponent<SphereCollider>();

        currentHealth = startingHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<Player>().getHealth();;
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        MoveTowardsPlayer();
        if (isAttacking)
        {
            AttackPlayer();
        }
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            isAttacking = true;
            nav.isStopped = true;
            anim.SetBool("IsMoving", false);

        }
    }
    void OnTriggerExit(Collider other)
    {

        isAttacking = false;
        nav.isStopped = false;
        anim.SetBool("IsMoving", true);

    }

    void AttackPlayer()
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

        isDead = true;
        sphereCollider.isTrigger = true;
        float varyAnimation = UnityEngine.Random.Range(0f, 2f);
        anim.SetTrigger("Dead" + Convert.ToString(varyAnimation));
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

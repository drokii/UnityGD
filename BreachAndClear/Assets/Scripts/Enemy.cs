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
    CameraShake cam;

    bool isDead;
    bool isSinking;

    float attackTimer;


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
        cam = player.transform.Find("Main Camera").GetComponent<CameraShake>();
    }

    void Update()
    {
        if (!GetComponent<MoveTowardsTarget>().enabled)
        {
            Attack();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {

            if (!isDead)
            {
                GetComponent<MoveTowardsTarget>().enabled = false;
            }

            anim.SetBool("IsMoving", false);


        }
    }
    //void OnTriggerExit(Collider other)
    //{

    //    if (!isDead)
    //    {
    //        GetComponent<MoveTowardsTarget>().enabled = true;
    //    }

    //}

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

        
        if (attackTimer >= 2f)
        {
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
            anim.SetTrigger(attack);
            attackTimer = 0f;
            player.gameObject.GetComponent<Health>().TakeDamage(25);
            cam.Enable();
        }
    }
    void Death()
    {
        System.Random r = new System.Random();

        isDead = true;
        if (nav.isOnNavMesh)
        {
            nav.isStopped = true;
        }


        int varyAnimation = r.Next(1, 3);
        anim.SetTrigger("Dead" + Convert.ToString(varyAnimation));

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        
        Destroy(gameObject, 2f);
    }

    private void MoveTowardsPlayer()
    {
        if (currentHealth > 0)
        {
            GetComponent<MoveTowardsTarget>().enabled = true;
        }
        else
        {
            GetComponent<MoveTowardsTarget>().enabled = false;
        }
    }
}

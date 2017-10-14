using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject objectToInstantiate;
    public float spawnDelay = 5f;
    private float spawnTimer;
    private int nrSpawns;
    public float limitNrSpawns;
    private bool spawnActive;
    private PlayerMovement playermov;
    public int NrSpawns
    {
        get
        {
            return nrSpawns;
        }

    }


    void Start()
    {
        playermov = GameObject.Find("Player").GetComponent<PlayerMovement>();

        spawnTimer = 0f;
        spawnActive = false;

    }

    // Update is called once per frame
    void Update()
    {


        if (spawnActive)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnDelay && nrSpawns < limitNrSpawns)
            {
                Spawn();
            }

            if (transform.childCount == 0 && nrSpawns >= limitNrSpawns)
            {
                playermov.MovePlayer();
                Destroy(gameObject, 0f);
            }

        }


    }

    void OnTriggerEnter()
    {
        spawnActive = true;
    }

    void Spawn()
    {
        spawnTimer = 0f;
        Instantiate(objectToInstantiate, transform.position, transform.rotation, transform);
        nrSpawns += 1;
    }
}

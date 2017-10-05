using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject objectToInstantiate;
    public float spawnDelay=5f;
    private float spawnTimer;


	// Use this for initialization
	void Start ()
	{
	    spawnTimer = 0f;

	}
	
	// Update is called once per frame
	void Update ()
	{
	    spawnTimer += Time.deltaTime;

	    if (spawnTimer >= spawnDelay)
	    {
	        spawnTimer = 0f;
            Spawn();
	    }


	}

    void Spawn()
    {
        Instantiate(objectToInstantiate, transform.position, transform.rotation);
    }
}

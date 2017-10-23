using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class PlayerMovement : MonoBehaviour
{
    public List<Transform> WayPoints;
    private int wayPointTracker;
    private NavMeshAgent PlayerNav;
    private LevelGeneration levelGen;
    public List<AudioClip> audioclipList;
    private AudioSource audioSource;
    private Vector3 currpos;
    private Vector3 lastpos;
    // Use this for initialization
    void Start()
    {
        PlayerNav = GetComponent<NavMeshAgent>();
        levelGen = GameObject.Find("World").GetComponent<LevelGeneration>();
        WayPoints = levelGen.WayPoints;
        audioSource = GetComponent<AudioSource>();
    }

    public void MovePlayer()
    {
        //Move player to next waypoint
        if (wayPointTracker < WayPoints.Count)
        {
            PlayerNav.destination = WayPoints[wayPointTracker].position;
            wayPointTracker++;
        }
    }
    void Update()
    {
        lastpos = currpos;
        currpos = transform.position;
        if (!audioSource.isPlaying && currpos != lastpos)
        {
            AudioClip footstep = audioclipList.ElementAt(Random.Range(0, audioclipList.Count));
            audioSource.clip = footstep;
            audioSource.PlayOneShot(footstep, 1);

        }
        

    }

}

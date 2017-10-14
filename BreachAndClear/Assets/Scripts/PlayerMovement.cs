using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMovement : MonoBehaviour
{
    public List<Transform> WayPoints;
    private int wayPointTracker;
    private NavMeshAgent PlayerNav;
    private LevelGeneration levelGen;

    // Use this for initialization
    void Start ()
    {
        PlayerNav = GetComponent<NavMeshAgent>();
        levelGen = GameObject.Find("World").GetComponent<LevelGeneration>();
        WayPoints = levelGen.WayPoints;
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

}

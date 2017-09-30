using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

enum gameStatus
{
    Start,
    Moving,
    Shooting
}

public class GameLogic : MonoBehaviour
{

    private gameStatus status = gameStatus.Start;
    public Transform WorldOrigin;
    public GameObject Player;
    private NavMeshAgent PlayerNav;
    public List<Transform> WayPoints;
    public GameObject[] Sections;
    public GameObject lastGeneratedSection;
    public GameObject startingRoom;

    private int wayPointTracker;

    public Canvas UICanvas;



    // Use this for initialization
    void Awake()
    {
        WorldOrigin = GetComponent<Transform>();
        PlayerNav = Player.GetComponent<NavMeshAgent>();
        lastGeneratedSection = startingRoom;
        GenerateNewSection(2);

    }

    // Update is called once per frame
    void Update()
    {

        if (status == gameStatus.Moving)
        {
            MovePlayer();
        }

    }

    public void NextStage()
    {
        if (status == gameStatus.Shooting || status == gameStatus.Start)
        {
            status = gameStatus.Moving;
        }


    }

    private void MovePlayer()
    {
        //Move player to next waypoint
        if (wayPointTracker < WayPoints.Count)
        {
            PlayerNav.destination = WayPoints[wayPointTracker].position;
            wayPointTracker++;
        }
        status = gameStatus.Shooting;
    }

    public void GenerateNewSection(int length)
    {
        for (int i = 0; i < length; i++)
        {
            //1- Choose which section to spawn
            GameObject nextSection = Sections[0];// plug Random.Range(0, x) here later on

            //2 - Spawn it at prefab endpoint
            Transform nextSectionSpawnPoint = lastGeneratedSection.transform.Find("EndSection");
            GameObject newSection = Instantiate(nextSection, nextSectionSpawnPoint);

            //3- Add navigation nodes of new prefab to navigation node list
            foreach (Transform t in newSection.transform)
            {
                if (t.name == "Waypoints")
                {
                    foreach (Transform t2 in t)
                    {
                        WayPoints.Add(t2);
                    }
                }
            }
            //4- Reassign new section to lastgenerated field
            lastGeneratedSection = newSection;

            //5- Spawn enemies

        }

    }

}

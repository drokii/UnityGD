using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

enum gameStatus{
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

	private int wayPointTracker;

    public Canvas UICanvas;



	// Use this for initialization
	void Awake ()
	{
        WorldOrigin = GetComponent<Transform>();
	    PlayerNav = Player.GetComponent<NavMeshAgent>();
        status = gameStatus.Start;


	}
	
	// Update is called once per frame
	void Update () {

		if(status == gameStatus.Moving){
			MovePlayer ();
		}
		
	}

    public void NextStage()
    {
        if(status == gameStatus.Shooting || status == gameStatus.Start)
        {
            status = gameStatus.Moving;
        }
        

    }

	private void MovePlayer()
	{		
		if (wayPointTracker < WayPoints.Count) {
			PlayerNav.destination = WayPoints [wayPointTracker].position;
			wayPointTracker++;
		}
		status = gameStatus.Shooting;
	}

	public void StageCompleted(){

        GameObject newSection;

        Transform origin = WorldOrigin.Find("Room1").Find("EndofSegment");


        newSection = Instantiate(Sections[0], origin.position, WorldOrigin.rotation);
        foreach (Transform t in newSection.transform)
        {
            if(t.name == "Waypoints")
            {
                foreach (Transform t2 in t)
                {
                    WayPoints.Add(t2);
                }
            }
        }
	}

}

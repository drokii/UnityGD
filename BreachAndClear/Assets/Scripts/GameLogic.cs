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
	public GameObject Player;
    private NavMeshAgent PlayerNav;
    public Transform[] WayPoints;
	private int wayPointTracker;

    public Canvas UICanvas;



	// Use this for initialization
	void Awake ()
	{
	    PlayerNav = Player.GetComponent<NavMeshAgent>();


	}
	
	// Update is called once per frame
	void Update () {

		if(status == gameStatus.Moving){
			MovePlayer ();
		}
		
	}

    public void StartGame()
    {
		status = gameStatus.Moving;
        UICanvas.gameObject.SetActive(false);
    }

	private void MovePlayer()
	{		
		if (wayPointTracker <= WayPoints.Length) {
			PlayerNav.destination = WayPoints [wayPointTracker].position;
			wayPointTracker++;
		}
		status = gameStatus.Shooting;
	}

	private void StageCompleted(){
		status = gameStatus.Moving;
	}
}

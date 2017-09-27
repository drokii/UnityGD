using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private bool GameStart = false;
    private bool MovingMode;
    private bool ShootingMode;

    public GameObject Player;
    private NavMeshAgent PlayerNav;
    public Transform[] Path;

    public Canvas UICanvas;



	// Use this for initialization
	void Awake ()
	{
	    PlayerNav = Player.GetComponent<NavMeshAgent>();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        GameStart = true;
        UICanvas.gameObject.SetActive(false);
    }
}

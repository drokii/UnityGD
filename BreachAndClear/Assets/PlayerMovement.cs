using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRunning : MonoBehaviour
{

    private float MoveSpeed = 0.1f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(0, 0, MoveSpeed);
	}
}

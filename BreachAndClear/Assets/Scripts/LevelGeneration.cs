using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class LevelGeneration : MonoBehaviour
{

    public List<Transform> WayPoints;
    public GameObject[] Sections;
    public GameObject lastGeneratedSection;
    public GameObject startingRoom;

    

    public Canvas UICanvas;



    // Use this for initialization
    void Awake()
    {         
        lastGeneratedSection = startingRoom;
        GenerateNewSection(5);
    }
 
    public void GenerateNewSection(int length)
    {
        System.Random r = new System.Random();

        for (int i = 0; i < length; i++)
        {
            //1- Choose which section to spawn
            
            int randomNumber = r.Next(0, 4);
            GameObject nextSection = Sections[randomNumber];// plug Random.Range(0, x) here later on

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


        }

    }

}

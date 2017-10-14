using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private Health health;
	void Start ()
    {
        health = GameObject.Find("Player").GetComponent<Health>();
        GetComponent<Button>().onClick.AddListener(RestartLevel);              
        GetComponent<Button>().gameObject.SetActive(false);
    }

    void Update()
    {
        if (health.isDead)
        {
            GetComponent<Button>().gameObject.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GetComponent<Button>().gameObject.SetActive(false);
    }
}

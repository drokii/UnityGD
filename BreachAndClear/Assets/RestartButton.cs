using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour {

	void Start ()
    {
		GetComponent<Button>().onClick.AddListener(RestartLevel);
	}

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

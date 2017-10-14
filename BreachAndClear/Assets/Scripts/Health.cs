using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private int currentHealth;
    public bool isDead;


	// Use this for initialization
	void Start ()
	{
	    isDead = false;
	    currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
        } 

    }

    public void Die()
    {
        isDead = true;
    }
}

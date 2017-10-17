using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    private Text curAmmo;
    private Text maxAmmo;
    private Text weaponName;
    private Text reloadWarning;
    private PlayerShooting playerShooting;
    public GameObject player;


    
    void Awake ()
    {
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
	    curAmmo = transform.Find("CurrentAmmo").gameObject.GetComponent<Text>();
	    maxAmmo = transform.Find("MaxAmmo").gameObject.GetComponent<Text>(); ;
	    weaponName = transform.Find("CurrentWeapon").gameObject.GetComponent<Text>(); ;
	    reloadWarning = transform.Find("ReloadWarning").gameObject.GetComponent<Text>(); ;
	}

    void Start()
    {
        reloadWarning.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
        curAmmo.text = playerShooting.currentWeapon.CurrentAmmo.ToString();
        maxAmmo.text = playerShooting.currentWeapon.MaxAmmo.ToString();
        weaponName.text = playerShooting.currentWeapon.Name;

        if (playerShooting.reloading)
        {
            reloadWarning.gameObject.SetActive(true);
        }
        if(playerShooting.reloading)
        {
            reloadWarning.gameObject.SetActive(false);
        }
    }
    
}

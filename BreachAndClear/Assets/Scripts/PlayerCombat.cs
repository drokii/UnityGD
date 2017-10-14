﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public float range = 50f;                   
    float timer;
    public int shotdamage = 50;                         
    LineRenderer gunLine;                           
    Light gunLight;                                
    float effectsDisplayTime = .1f;                
    private PlayerControls pcontrols;
    private Vector2 touchPosition;
    public Camera camera;
    public GameObject hitParticles;
    public GameObject world;

    void Awake()
    {
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        pcontrols = GetComponentInParent<PlayerControls>();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (pcontrols.returnMovement == TouchMovement.TAP)
        {   
            Shoot();
        }

        if (timer >= effectsDisplayTime)
        {           
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;
        Ray shootRay;
        RaycastHit shootHit;
        touchPosition = pcontrols.actualTouch;

        Vector3 cameraShoot = new Vector3(touchPosition.x, touchPosition.y, 0);

        
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay = camera.ScreenPointToRay(cameraShoot);
        Debug.DrawRay(shootRay.origin, shootRay.direction);



        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            Enemy enemy = shootHit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(shotdamage, shootHit.point);
            }
            else
            {
                GameObject hit = Instantiate(hitParticles, shootHit.point, shootHit.transform.rotation);
                hitParticles.transform.position = shootHit.point;
                hitParticles.GetComponent<ParticleSystem>().Play();
                Destroy(hit,3f);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
        
        pcontrols.returnMovement = TouchMovement.NONE;
        

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public float range = 100f;                      // The distance the gun can fire.
    float timer;                                    // A timer to determine when to fire. 
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    LineRenderer gunLine;                           // Reference to the line renderer.
    Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = .1f;                // The proportion of the timeBetweenBullets that the effects will display for.
    private PlayerControls pcontrols;
    private Touch touch;
    public Camera camera;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        pcontrols = GetComponentInParent<PlayerControls>();
        

    }

    void Update()
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
        touch = pcontrols.lastTouch;

        Vector3 cameraShoot = new Vector3(touch.position.x, touch.position.y, 0);
        gunLight.enabled = true;

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay = camera.ScreenPointToRay(cameraShoot);
        Debug.DrawRay(shootRay.origin, shootRay.direction);



        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
        
        pcontrols.returnMovement = TouchMovement.NONE;
        

    }
}

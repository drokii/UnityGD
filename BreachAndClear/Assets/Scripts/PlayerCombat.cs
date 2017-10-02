using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public float range = 50f;                   
    float timer;                                   
    LineRenderer gunLine;                           
    Light gunLight;                                
    float effectsDisplayTime = .1f;                
    private PlayerControls pcontrols;
    private Vector2 touchPosition;
    public Camera camera;


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

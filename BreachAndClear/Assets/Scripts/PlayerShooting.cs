using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public enum EWeapon
{
    Pistol,
    Rifle
}
public class PlayerShooting : MonoBehaviour
{

    public float range = 50f;
    float timer;
    private float reloadTimer = 2f;
    private float fireRateTimer = 0.5f;

    public Weapon currentWeapon;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = .1f;

    private PlayerControls pcontrols;
    private Vector2 touchPosition;

    public Camera camera;
    public GameObject hitParticles;
    public bool reloading;

    void Awake()
    {
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        pcontrols = GetComponentInParent<PlayerControls>();
        currentWeapon = new Weapon(EWeapon.Pistol);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (pcontrols.returnMovement == TouchMovement.SWIPEDOWN && currentWeapon.CurrentAmmo != currentWeapon.MaxAmmo)
        {
            reloading = true;
            Reload();
        }

        if (pcontrols.returnMovement == TouchMovement.TAP)
        {
            if (timer >= fireRateTimer)
            {
                if (currentWeapon.CurrentAmmo > 0 && !reloading)
                {
                    Shoot();
                    currentWeapon.CurrentAmmo -= 1;
                }
                else
                {
                    reloading = true;
                    Reload();
                }
            }


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

        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            Enemy enemy = shootHit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(currentWeapon.Damage, shootHit.point);
            }
            else
            {
                GameObject hit = Instantiate(hitParticles, shootHit.point, shootHit.transform.rotation);
                hitParticles.transform.position = shootHit.point;
                hitParticles.GetComponent<ParticleSystem>().Play();
                Destroy(hit, 3f);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    void Reload()
    {

        if (timer >= reloadTimer)
        {
           currentWeapon.Reload();
           reloading = false; 
        }
        

    }

}

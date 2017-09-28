using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public enum TouchMovement
{
    SWIPEUP,
    SWIPEDOWN,
    SWIPELEFT,
    SWIPERIGHT,
    HOLD,
    TAP,
    NONE

}
public class PlayerControls : MonoBehaviour
{


    Vector2 firstTouchPosition = Vector2.zero;
    Vector2 finalFingerPosition = Vector2.zero;
    private float accelerometerRead;
    public TouchMovement returnMovement = TouchMovement.NONE;
    public Touch lastTouch;

    private Rigidbody playerRbody;
    void Start()
    {
        playerRbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectAccelerometer();
        DetectTouchMovement();
        MoveCharacter();
        if (returnMovement != TouchMovement.NONE)
        {
            Debug.Log(Convert.ToString(returnMovement) + " = Touch Input; Accelerometer is =" + Convert.ToString(accelerometerRead));           
        }
        
        
    }

    private void DetectTouchMovement()
    {



        if (Input.touchCount != 0)
        {
            float timeHeld =+ Time.deltaTime;
            returnMovement = TouchMovement.NONE;

            Touch t = Input.GetTouch(0);
            lastTouch = t;

            if (t.phase == TouchPhase.Began)
            {
                firstTouchPosition = t.position;
            }

            if (t.phase == TouchPhase.Stationary && timeHeld <= 1.5f)
            {
                returnMovement = TouchMovement.HOLD;
                
            }


            if (t.phase == TouchPhase.Ended && firstTouchPosition != Vector2.zero)
            {
                finalFingerPosition = t.position - firstTouchPosition;
                finalFingerPosition.Normalize();

                if (finalFingerPosition.y > 0 && finalFingerPosition.x > -0.3f && finalFingerPosition.x < 0.3f) // (1,1)
                {
                    returnMovement = TouchMovement.SWIPEUP;
                }

                if (finalFingerPosition.y < 0 && finalFingerPosition.x > -0.3f && finalFingerPosition.x < 0.3f)
                {
                    returnMovement = TouchMovement.SWIPEDOWN;
                }

                if (finalFingerPosition.x > 0 && finalFingerPosition.y > -0.3f && finalFingerPosition.y < 0.3f)
                {
                    returnMovement = TouchMovement.SWIPERIGHT;
                }

                if (finalFingerPosition.x < 0 && finalFingerPosition.y > -0.3f && finalFingerPosition.y < 0.3f)
                {
                    returnMovement = TouchMovement.SWIPELEFT;
                }

                if (returnMovement == TouchMovement.NONE)
                {
                    returnMovement = TouchMovement.TAP;
                }
					
                firstTouchPosition = Vector2.zero;
                finalFingerPosition = Vector2.zero;
                
            }
        }
    }


    void DetectAccelerometer()
    {
        Vector2 accelerometer = Input.acceleration;
        accelerometerRead = accelerometer.x;
    }

    void MoveCharacter()
    {
        switch (returnMovement)
        {
            case TouchMovement.SWIPEDOWN:
                playerRbody.transform.Translate(Vector3.back * Time.deltaTime * 5);
                break;
            case TouchMovement.SWIPEUP:
                playerRbody.transform.Translate(Vector3.forward * Time.deltaTime * 5);
                break;
            case TouchMovement.SWIPELEFT:
                playerRbody.transform.Translate(Vector3.left * Time.deltaTime * 5);
                break;
            case TouchMovement.SWIPERIGHT:
                playerRbody.transform.Translate(Vector3.right * Time.deltaTime * 5);
                break;
            case TouchMovement.TAP:
                playerRbody.transform.Translate(Vector3.zero);
                break;
            default:

                break;
        }
    }
}

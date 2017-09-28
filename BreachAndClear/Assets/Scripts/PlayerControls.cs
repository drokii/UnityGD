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
	private float timeHeld;
    void Start()
    {
        playerRbody = GetComponent<Rigidbody>();
        timeHeld = 0;
    }

    // Update is called once per frame

    void Update()
    {
        //DetectAccelerometer();
		

        if (Input.touchCount != 0)
        {
            lastTouch = Input.GetTouch(0);
            timeHeld += Time.deltaTime;
        }

        DetectTouchMovement();
        //MoveCharacter();

        if (returnMovement != TouchMovement.NONE)
        {
            Debug.Log(Convert.ToString(returnMovement) + " = Touch Input; Accelerometer is =" + Convert.ToString(accelerometerRead));
            if (returnMovement != TouchMovement.HOLD)
            {
                returnMovement = TouchMovement.NONE;
            }      
        }
        


    }

    private void DetectTouchMovement()
    {         

            if (lastTouch.phase == TouchPhase.Began)
            {
                firstTouchPosition = lastTouch.position;
            }

            if (lastTouch.phase == TouchPhase.Stationary && timeHeld >= 2f)
            {
                returnMovement = TouchMovement.HOLD;
				
                
            }


            if (lastTouch.phase == TouchPhase.Ended && firstTouchPosition != Vector2.zero)
            {
                Vector2 lastTouchNormalized = lastTouch.position.normalized;
                firstTouchPosition.Normalize();
                finalFingerPosition = lastTouchNormalized - firstTouchPosition;
                

				if (finalFingerPosition.y > 0 && finalFingerPosition.x > -0.3f && finalFingerPosition.x < 0.3f && timeHeld >= 0.1f) 
                {
                    returnMovement = TouchMovement.SWIPEUP;
					timeHeld = 0f;
                }

				if (finalFingerPosition.y < 0 && finalFingerPosition.x > -0.3f && finalFingerPosition.x < 0.3f && timeHeld >= 0.1f)
                {
                    returnMovement = TouchMovement.SWIPEDOWN;
					timeHeld = 0f;
                }

				if (finalFingerPosition.x > 0 && finalFingerPosition.y > -0.3f && finalFingerPosition.y < 0.3f && timeHeld >= 0.1f)
                {
                    returnMovement = TouchMovement.SWIPERIGHT;
					timeHeld = 0f;
                }

				if (finalFingerPosition.x < 0 && finalFingerPosition.y > -0.3f && finalFingerPosition.y < 0.3f && timeHeld >= 0.1f)
                {
                    returnMovement = TouchMovement.SWIPELEFT;
					timeHeld = 0f;
                }

                if (returnMovement == TouchMovement.NONE)
                {
                    returnMovement = TouchMovement.TAP;
					timeHeld = 0f;
                }

                
					
                firstTouchPosition = Vector2.zero;
                finalFingerPosition = Vector2.zero;
                
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
                //playerRbody.transform.Translate(Vector3.back * Time.deltaTime * 5);
                break;
            case TouchMovement.SWIPEUP:
                //playerRbody.transform.Translate(Vector3.forward * Time.deltaTime * 5);
                break;
            case TouchMovement.SWIPELEFT:
                //playerRbody.transform.Translate(Vector3.left * Time.deltaTime * 5);
                break;
            case TouchMovement.SWIPERIGHT:
                //playerRbody.transform.Translate(Vector3.right * Time.deltaTime * 5);
                break;
            case TouchMovement.TAP:
                //playerRbody.transform.Translate(Vector3.zero);
                break;
            default:

                break;
        }
    }
}

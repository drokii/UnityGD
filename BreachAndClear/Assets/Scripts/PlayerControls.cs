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
    private float accelerometerRead;
    private Rigidbody playerRbody;
    private float dragDistance;
    private float timeHeld;
    public TouchMovement returnMovement = TouchMovement.NONE;
    Vector2 firstTouch;
    private Vector2 touchDelta;

    public Vector2 actualTouch;


    void Start()
    {
        playerRbody = GetComponent<Rigidbody>();
        timeHeld = 0;
        dragDistance = 0.9f;
    }

    void Update()
    {
        //DetectAccelerometer();
        DetectTouchMovement();
        //MoveCharacter();
        DebugLog();

    }

    private void DebugLog() //  This is here until I implement functionality for all swipes.
    {
        if (returnMovement != TouchMovement.NONE)
        {
            Debug.Log(Convert.ToString(returnMovement) + " = Touch Input; Accelerometer is =" + Convert.ToString(accelerometerRead));

            if (returnMovement != TouchMovement.HOLD && returnMovement != TouchMovement.TAP)
            {
                returnMovement = TouchMovement.NONE;
            }
        }
    }

    private void DetectTouchMovement()
    {

        if (Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(0);
            timeHeld += Time.deltaTime;

            if (touch.phase == TouchPhase.Began)
            {                
                
            }

            if (touch.phase == TouchPhase.Stationary && timeHeld >= 1.5f)
            {
                returnMovement = TouchMovement.HOLD;
                timeHeld = 0;

            }


            if (touch.phase == TouchPhase.Ended)
            {
                timeHeld = 0;
                touchDelta = touch.deltaPosition.normalized;
                float absoluteDeltaX = Mathf.Abs(touchDelta.x);
                float absoluteDeltaY = Mathf.Abs(touchDelta.y);
                actualTouch = touch.position; 

                if (absoluteDeltaX >= dragDistance || absoluteDeltaY >= dragDistance)
                {
                    //swipe direction check
                    if (absoluteDeltaX > absoluteDeltaY) // horizontal check
                    {
                        if (touchDelta.x < 0)
                        {
                            returnMovement = TouchMovement.SWIPELEFT;
                        }
                        if (touchDelta.x > 0)
                        {
                            returnMovement = TouchMovement.SWIPERIGHT;
                        }

                    }
                    if (absoluteDeltaX < absoluteDeltaY)// vertical
                    {
                        if (touchDelta.y < 0)
                        {
                            returnMovement = TouchMovement.SWIPEDOWN;
                        }
                        if (touchDelta.y > 0)
                        {
                            returnMovement = TouchMovement.SWIPEUP;
                        }
                    }
                }
                else
                {
                    returnMovement = TouchMovement.TAP;
                }
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

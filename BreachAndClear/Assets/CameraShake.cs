using UnityEngine;
using System.Collections;
using System;

public class CameraShake : MonoBehaviour
{

    public float linearIntensity = 0.25f;
    public float angularIntensity = 5f;
    public float shakeduration = 0.25f;

    public bool isShaking = false;

    private bool angularShaking = true;
    private float timer;

    void Update()
    {
        if (isShaking && timer < shakeduration)
        {
            timer += Time.deltaTime;
            LinearShaking();
            if (angularShaking)
                AngularShaking();

        }
        else
        {
            Disable();
            timer = 0f;
        }

        
    }

    private void LinearShaking()
    {
        Vector2 shake = UnityEngine.Random.insideUnitCircle * linearIntensity;
        Vector3 newPosition = transform.localPosition;
        newPosition.x = shake.x;
        newPosition.y = shake.y;
        transform.localPosition = newPosition;
    }

    private void AngularShaking()
    {
        float shake = UnityEngine.Random.Range(-angularIntensity, angularIntensity);
        transform.localRotation = Quaternion.Euler(0f, 0f, shake);
    }

    public void SetAngularShaking(bool state)
    {
        angularShaking = state;
        if (!angularShaking)
            transform.localRotation = Quaternion.identity;
    }

    public void Enable()
    {        
        isShaking = true;
    }

    public void Disable()
    {
        isShaking = false;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}

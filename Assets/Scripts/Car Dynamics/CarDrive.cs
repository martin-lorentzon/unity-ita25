using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CarDrive : MonoBehaviour
{
    [Header("m/s/s")]
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float groundFriction = 1f;

    [Header("m/s")]
    public float topSpeedFwd = 30f;
    public float topSpeedRev = 10f;

    public float wheelRadius = 0.35f;
    public Transform[] wheels;


    [NonSerialized]
    public float currentSpeed;


    void FixedUpdate()
    {
        // Läs input
        float vInput = Input.GetAxis("Vertical");

        bool isBraking = IsBraking(vInput, currentSpeed);
        bool isCoasting = Mathf.Approximately(vInput, 0f);

        if (isBraking)  // Flytta hastigheten mot noll med bilens deceleration
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        else if (isCoasting)  // Flytta hastigheten mot noll med markens friktion
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, groundFriction * Time.fixedDeltaTime);
        else  // Plussa på accelerationen på hastigheten
            currentSpeed += acceleration * vInput * Time.fixedDeltaTime;

        currentSpeed = Mathf.Clamp(currentSpeed, -topSpeedRev, topSpeedFwd);  // Förhåller currentSpeed till topSpeed
        transform.position += transform.forward * currentSpeed * Time.fixedDeltaTime;  // Flytta bilen framåt med currentSpeed

        UpdateVisuals();
    }

    bool IsBraking(float vInput, float currentSpeed)
    {
        if (Mathf.Approximately(vInput, 0f))  // Bromsa ej utan någon input (dvs då vInput är noll)
            return false;

        if (Mathf.Approximately(currentSpeed, 0f))  // Bromsa ej då vi står stilla
            return false;

        return Mathf.Sign(vInput) != Mathf.Sign(currentSpeed);  // Bromsa när vi rullar framåt och trycker bakåt (jämför riktining av dessa)
    }

    void UpdateVisuals()
    {
        float wheelCircumference = 2 * Mathf.PI * wheelRadius;
        float rotationAngle = (currentSpeed / wheelCircumference) * 360f * Time.fixedDeltaTime;

        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(rotationAngle, 0f, 0f);
        }
    }
}

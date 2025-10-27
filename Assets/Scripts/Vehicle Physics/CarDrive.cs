using System;
using UnityEngine;

public class CarDrive : MonoBehaviour
{
    [Header("m/s/s")]
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float groundFriction = 1f;

    [Header("m/s")]
    public float topSpeedFwd = 30f;
    public float topSpeedRev = 10f;

    [NonSerialized]
    public float currentSpeed;

    [Header("Visuals")]
    public Transform[] wheels;
    public float wheelRadius = 0.35f;

    // Fysik (ej beroende av FPS)
    void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical");

        bool isBraking = IsBraking(vInput, currentSpeed);
        bool isCoasting = Mathf.Approximately(vInput, 0);

        if (isBraking)
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        else if (isCoasting)
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, groundFriction * Time.fixedDeltaTime);
        else
            currentSpeed += acceleration * vInput * Time.fixedDeltaTime;

        currentSpeed = Mathf.Clamp(currentSpeed, -topSpeedRev, topSpeedFwd);
        transform.position += transform.forward * currentSpeed * Time.fixedDeltaTime;

        UpdateVisuals();
    }

    bool IsBraking(float vInput, float currentSpeed)
    {
        if (Mathf.Approximately(vInput, 0f))
            return false;
        if (Mathf.Approximately(currentSpeed, 0f))
            return false;
        return Mathf.Sign(vInput) != Mathf.Sign(currentSpeed);
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
using UnityEngine;

[RequireComponent(typeof(CarDrive))]
public class CarSteering : MonoBehaviour
{
    CarDrive carDrive;
    float steeringRate;

    // Public variables
    public float turningRadius = 5f;

    [Header("Visuals")]
    public Transform[] steeringEmpties;
    public float visualSteeringAngle = 30f;

    void Start()
    {
        carDrive = GetComponent<CarDrive>();
    }

    void FixedUpdate()
    {
        float hInput = Input.GetAxis("Horizontal");

        // angularVelocity (rad/s) = linearVelocity / radius
        steeringRate = (carDrive.currentSpeed / turningRadius) * Mathf.Rad2Deg;

        transform.Rotate(0f, steeringRate * hInput * Time.fixedDeltaTime, 0f);

        UpdateVisuals(hInput);
    }

    void UpdateVisuals(float hInput)
    {
        foreach (Transform steeringEmpty in steeringEmpties)
        {
            steeringEmpty.localEulerAngles = steeringEmpty.up * visualSteeringAngle * hInput;
        }
    }

    void OnDrawGizmos()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Approximately(hInput, 0))
            return;

        float distance = turningRadius * Mathf.Sign(hInput);
        Gizmos.DrawRay(transform.position, transform.right * distance);
    }
}

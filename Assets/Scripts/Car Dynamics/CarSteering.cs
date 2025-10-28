using UnityEngine;

public class CarSteering : MonoBehaviour
{

    public float turningRadius = 5f;
    public Transform[] steeringEmpties;
    public float visualSteeringAngle;

    // Private variables
    float steeringRate;
    CarDrive carDrive;

    void Start()
    {
        carDrive = GetComponent<CarDrive>();
    }



    void FixedUpdate()
    {
        float hInput = Input.GetAxis("Horizontal");

        // angularVelocity (rad/s) = linearVelocity / radius
        steeringRate = (carDrive.currentSpeed / turningRadius) * Mathf.Rad2Deg;

        transform.Rotate(transform.up, steeringRate * hInput * Time.fixedDeltaTime);

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

        if (Mathf.Approximately(hInput, 0f))
            return;

        float distance = turningRadius * hInput;
        Gizmos.DrawRay(transform.position, transform.right * distance);
    }

    
}

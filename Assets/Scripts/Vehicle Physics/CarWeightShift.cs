using UnityEngine;

public class CarWeightShift : MonoBehaviour
{
    public Transform carBody;

    // Private variables
    Vector3 previousPosition;
    Vector3 previousVelocity;

    void Start()
    {
        previousPosition = transform.position;
        previousVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 velocity = (transform.position - previousPosition) / Time.fixedDeltaTime;
        
        Vector3 acceleration = (velocity - previousVelocity) / Time.fixedDeltaTime;
        Vector3 localAcceleration = transform.InverseTransformDirection(acceleration);

        


        Vector3 startPoint = transform.position + Vector3.up * 2f;
        Vector3 endPoint = startPoint + localAcceleration * 5f;
        Debug.Log(localAcceleration);
        Debug.DrawLine(startPoint, endPoint);

        carBody.localEulerAngles = new Vector3(-localAcceleration.z * 1.0f, 0f, localAcceleration.x * 1.0f);

        previousPosition = currentPosition;
        previousVelocity = velocity;
    }
}

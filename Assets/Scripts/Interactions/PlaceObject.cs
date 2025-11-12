using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public void Place(Rigidbody grabbedRigidbody, float maxDistance)
    {
        GameObject[] snappingPoints = GameObject.FindGameObjectsWithTag("SnappingPoint");


        // Gets the closest snapping point
        GameObject closestSnappingPoint = null;
        float closestSnappingDistance = Mathf.Infinity;

        foreach (GameObject snap in snappingPoints)
        {
            float snapDistance = Vector3.Distance(grabbedRigidbody.position, snap.transform.position);

            if (snapDistance < closestSnappingDistance)
            {
                closestSnappingPoint = snap;
                closestSnappingDistance = snapDistance;
            }
        }

        if (closestSnappingDistance > maxDistance)  // Cannot reach any snapping point
            return;

        grabbedRigidbody.isKinematic = true;
        grabbedRigidbody.transform.parent = closestSnappingPoint.transform;
        grabbedRigidbody.transform.localEulerAngles = Vector3.zero;
        grabbedRigidbody.transform.localPosition = Vector3.zero;
    }
}

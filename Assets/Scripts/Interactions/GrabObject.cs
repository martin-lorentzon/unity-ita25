using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public new Camera camera;
    public Transform grabTransform;

    // public PlaceObject placeOjectComponent;

    private Rigidbody grabbedRigidbody = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            if (grabbedRigidbody == null)
                Grab();
            else
                Release();  // Vänta med denna
    }

    void Grab()
    {
        // Vilket objekt kollar spelaren på? Vi raycaster från kameran framåt
        // ...det objekt vår raycast träffar ska vi plocka upp
        RaycastHit hitInfo;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        // Om vi inte får någon träff... Dvs om *inte* raycast-
        // ...ut från denna får vi vår hitInfo
        if (!Physics.Raycast(ray, out hitInfo, 6f))
            return;

        // Kolla så att objekt går att plocka upp.
        if (!hitInfo.transform.CompareTag("Grabbable"))
            return;

        // Nu ska vi få objektet att röra sig med kameran

        // Då är vi intresserade av fysikkomponenten på det objekt vi plockade upp
        // ...och det är dess rigidbody (det vi ska flytta på)
        grabbedRigidbody = hitInfo.collider.attachedRigidbody;
        

        grabbedRigidbody.isKinematic = true;
        // Ska fysiken påverka objektet? Kanske-
        // ...det här går att göra helt fysikbaserat.
        // Men vi gör det simpelt för oss och säger nej, stäng av fysiken!

        grabbedRigidbody.position = grabTransform.position;
        grabbedRigidbody.transform.parent = camera.transform;
        // Flytta objektet till vår GrabTransform och låt det följa kameran

        grabbedRigidbody.transform.localRotation = grabTransform.localRotation;
        // Kopiera rotationen av GrabTransform

        // Testkör redan här

        grabbedRigidbody.GetComponent<Collider>().enabled = false;
    }

    void Release()
    {
        grabbedRigidbody.GetComponent<Collider>().enabled = true;
        grabbedRigidbody.isKinematic = false;
        grabbedRigidbody.transform.parent = null;

        // if (placeOjectComponent != null)
            // placeOjectComponent.Place(grabbedRigidbody, 3f);

        grabbedRigidbody = null;
    }
}

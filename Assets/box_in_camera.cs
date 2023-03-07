using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_in_camera : MonoBehaviour
{
    public Camera temp;
    MeshRenderer renderer2;
    Plane[] cameraFrustum; //series of camera planes
    Collider collider2;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Camera c in Camera.allCameras){
            Debug.Log("uit");

            Debug.Log(c.gameObject.name);
            if ( "test_camera" == c.gameObject.name)
            {
                Debug.Log("in");

                Debug.Log(c.gameObject.name);

                temp = c;
            }
            else
            {
                Debug.Log("Oops");
            }



        }
        

        renderer2 = GetComponent<MeshRenderer>();
        collider2 = GetComponent<Collider>();
        var bounds = collider2.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(temp); //returns the planes for the camera --> place it in update if your camera moves
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            renderer2.sharedMaterial.color = Color.green;
        }
        else
        {
            renderer2.sharedMaterial.color = Color.red;

        }
    }


    void Update()
    {

        var bounds = GetComponent<Collider>().bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(temp); //returns the planes for the camera --> place it in update if your camera moves
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            renderer2.sharedMaterial.color = Color.green;
        }
        else
        {
            renderer2.sharedMaterial.color = Color.red;

        }
    }
}



// Start is called before the first frame update

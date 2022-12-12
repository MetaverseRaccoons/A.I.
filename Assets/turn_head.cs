using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn_head : MonoBehaviour
{
   
    // Start is called before the first frame update
    //void Start()
   // {
   // }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 rotationToAdd = new Vector3(0, 20 * Time.deltaTime,0);
            transform.Rotate(rotationToAdd);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Vector3 rotationToAdd = new Vector3(0, -20 * Time.deltaTime,0) ;
            transform.Rotate(rotationToAdd);

        }
    }
}



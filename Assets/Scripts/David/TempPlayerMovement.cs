using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement : MonoBehaviour
{
    public float speed;
    

    // Update is called once per frame
    void Update()
    {
        
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontal,0f,vertical);

        if(movement.magnitude > 0.1){
            transform.Translate(movement * speed, Space.World);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSwing : MonoBehaviour
{
    public int swingForce;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            animator.SetTrigger("Swing");
            
         //   this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * swingForce);
        }
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTestMovement : MonoBehaviour
{
    Vector3 pos;
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        pos.y = transform.position.y;
        pos.y = Mathf.PingPong(Time.time, 2);
        transform.position = pos;
    }
}

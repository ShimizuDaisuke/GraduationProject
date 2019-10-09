using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // speedを制御する
    public float speed = 10;
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        // xとzにspeedを掛ける
        rigidbody.AddForce(x * speed, 0, z * speed);
    }
}

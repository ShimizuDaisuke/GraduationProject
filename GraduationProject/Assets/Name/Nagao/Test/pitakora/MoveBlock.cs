using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    //Rigidbody
    private Rigidbody rigid;

    //初期値
    private Vector3 defaultPos;

    //揺れ幅
    [SerializeField]
    private float width = 2.0f;

    float a;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        defaultPos = transform.position;
    }

    void FixedUpdate()
    {

        a += Time.deltaTime;

        rigid.MovePosition(new Vector3(defaultPos.x, defaultPos.y + (Mathf.Sin(a) * width), defaultPos.z));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    float degree = 0;
   
    void Start()
    {
        int a = 1;
    }

   
    void Update()
    {

        degree +=10;

        transform.rotation = Quaternion.Euler(Mathf.Sin(Mathf.Deg2Rad * degree), Mathf.Cos(Mathf.Deg2Rad * degree), degree);
        transform.position = new Vector3(407.0f+Mathf.Sin(Mathf.Deg2Rad * degree), transform.position.y, transform.position.z);     
    }
}

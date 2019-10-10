using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRSpotObject : MonoBehaviour
{
    GameObject gm;

    SampleQRReader spot;

    void Start()
    {
        gm = GameObject.FindWithTag("QRDirector");
        spot = gm.gameObject.GetComponent<SampleQRReader>();
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            spot.QRSpot = true;
        }
    }
}

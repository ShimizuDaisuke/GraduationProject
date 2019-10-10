using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    GameObject qRDirector;

    SampleQRReader spot;

    void Start()
    {
        qRDirector = GameObject.FindWithTag("QRDirector");

        spot = qRDirector.gameObject.GetComponent<SampleQRReader>();
    }

    public void OnClick()
    {
        spot.QRSpot = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    GameObject gm;

    SampleQRReader spot;

    void Start()
    {
        gm = GameObject.FindWithTag("GameObject");

        spot = gm.gameObject.GetComponent<SampleQRReader>();
    }

    public void OnClick()
    {
        spot.QRSpot = false;
    }
}

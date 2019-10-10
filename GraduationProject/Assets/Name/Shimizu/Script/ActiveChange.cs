using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChange : MonoBehaviour
{
    [SerializeField]
    GameObject cameraImage;

    [SerializeField]
    GameObject text;

    public GameObject CameraImage
    {
        get { return cameraImage; }
        set { cameraImage = value; }
    }

    public GameObject Text
    {
        get { return text; }
        set { text = value; }
    }

}

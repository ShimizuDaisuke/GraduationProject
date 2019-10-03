//=======================================================================================
//! @file SampleQRReader
//! @brief カメラを起動してQRコードを読み込むクラス
//! @author 志水大輔
//! @date 9/26
//! @note 解読中
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SampleQRReader : MonoBehaviour
{
    public Camera mainCamera;

    WebCamTexture _webCam;

    const int FPS = 60;

    string _result = null;

    bool _switch = false;

    IEnumerator Start()
    {
        mainCamera = Camera.main;
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if(Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        {
            Debug.LogFormat("no camera.");
            yield break;
        }
        Debug.LogFormat("camera ok.");
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices == null || devices.Length == 0)
        {
            yield break;
        }

        //Quadを画面いっぱいに広げる
        float _h = mainCamera.orthographicSize * 2;
        float _w = _h * mainCamera.aspect;
        // スマホが横ならそのまま
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            transform.localScale = new Vector3(_h, _w, 1);
        }
        // 縦なら回転させる
        if (Input.deviceOrientation == DeviceOrientation.FaceUp)
        {
            transform.localScale = new Vector3(_h, _w, 1);
            transform.localRotation *= Quaternion.Euler(0, 0, -90);
        }
        // カメラのテクスチャをQuadに乗せる
        Renderer rend = GetComponent<Renderer>();
        if (devices.Length > 0)
        {
            WebCamDevice cam = devices[0];
            WebCamTexture _wCam = new WebCamTexture(cam.name);

            rend.material.mainTexture = _webCam;
            _wCam.Play();
            int width = _wCam.width, height = _wCam.height;
            if (width < 1280 || height < 720)
            {
                width *= 2;
                height *= 2;
            }
            _webCam = new WebCamTexture(cam.name, Screen.width, Screen.height, 12);
            _wCam.Stop();

            rend.material.mainTexture = _webCam;

        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _switch = true;
            _webCam.Play();
            
        }

        if (Input.GetKey(KeyCode.Z))
        {
            _webCam.Stop();
            _switch = false;
        }

        if (_webCam != null)
        {
            if(_switch != false)
            {
                _result = QRCodeHelper.Read(_webCam);
                Debug.LogFormat("result : " + _result);
            }
            
        }
    }
}


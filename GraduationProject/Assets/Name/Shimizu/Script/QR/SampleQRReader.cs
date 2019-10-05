﻿//=======================================================================================
//! @file   SampleQRReader
//! @brief  カメラなどを起動して実行するクラス
//! @author 志水大輔
//! @date   9/26
//! @note   書き換える可能性あり
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SampleQRReader : MonoBehaviour
{
    // メインカメラ
    public Camera mainCamera;

    // 映像をテクスチャとして扱う
    WebCamTexture _webCam;

    // 読み込んだ結果格納
    private string _result = null;

    // カメラの On Off
    bool _switch = false;

    // FPS設定 カメラのインスタンスに使う
    const int FPS = 60;

    // カメラの回転
    const int ROTATION = -90;

    // 最大画面幅、高さ
    const int SCREEN_WIDTH = 1280;
    const int SCREEN_HEIGHT = 720;

    // 画面サイズ
    const int SCREEN_SIZE = 2;

    //=======================================================================================
    //! @brief 開始処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return 反復処理をサポートするインターフェース
    //=======================================================================================
    IEnumerator Start()
    {
        // メインカメラの情報格納
        mainCamera = Camera.main;

        // カメラを使用する際に許可を求める
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        // カメラの許可を行っているか
        if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        {
            Debug.LogFormat("no camera.");
            yield break;
        }
        Debug.LogFormat("camera ok.");

        // 利用可能なカメラのデバイス
        WebCamDevice[] devices = WebCamTexture.devices;

        // デバイスの情報がnullじゃないか確認
        if (devices == null || devices.Length == 0)
        {
            yield break;
        }

        // スマホ内での処理====================================================================
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
            transform.localRotation *= Quaternion.Euler(0, 0, ROTATION);
        }
        // ====================================================================================
        // カメラのテクスチャをQuadに乗せる
        Renderer rend = GetComponent<Renderer>();

        if (devices.Length > 0)
        {
            // デバイスの情報[0]を格納
            WebCamDevice cam = devices[0];

            // カメラに映っているテクスチャを追加する
            WebCamTexture _wCam = new WebCamTexture(cam.name);

            // MaterialTextureに映っているテクスチャを張り付ける
            rend.material.mainTexture = _webCam;

            // webカメラの起動しテクスチャを現在のレンダラーに割り当てる
            _wCam.Play();

            // 縦、横幅の計算
            int width = _wCam.width, height = _wCam.height;

            // もし実際の画面が小さいとき
            if (width < SCREEN_WIDTH || height < SCREEN_HEIGHT)
            {
                // 大きくする
                width *= SCREEN_SIZE;
                height *= SCREEN_SIZE;
            }
            // 指定したwebCameraをインスタンスする
            _webCam = new WebCamTexture(cam.name, Screen.width, Screen.height, FPS);
            _wCam.Stop();

            // MaterialTextureに代入
            rend.material.mainTexture = _webCam;
        }
    }

    //=======================================================================================
    //! @brief 更新処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void Update()
    {
        // カメラの起動
        if (Input.GetKey(KeyCode.Space))
        {
            // カメラのスイッチ ON
            _switch = true;
            // カメラの起動
            _webCam.Play();
        }

        // カメラを止める
        if (Input.GetKey(KeyCode.Z))
        {
            // カメラの終了
            _webCam.Stop();
            // カメラのスイッチ OFF
            _switch = false;
        }

        // 映像のテクスチャがあるか
        if (_webCam != null)
        {
            // カメラが起動していた場合
            if (_switch != false)
            {
                // 読み込んだ情報を格納
                _result = QRCodeHelper.Read(_webCam);
                if(_result != "error")
                {
                    // カメラの終了
                    _webCam.Stop();
                    // カメラのスイッチ OFF
                    _switch = false;
                }
                // 読み込んで格納した文字、数値をデバッグ表示
                Debug.LogFormat("result : " + _result);
                Debug.LogFormat(_result);
            }
        }
    }

    public string Result
    {
        get { return _result; }
        private set { _result = value; }
    }
}


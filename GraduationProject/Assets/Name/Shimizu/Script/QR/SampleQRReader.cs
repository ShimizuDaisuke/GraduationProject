//=======================================================================================
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
    // 映像をテクスチャとして扱う
    WebCamTexture _webCamTex;

    // 読み込んだ結果格納
    private string _result = null;

    // カメラの On Off
    bool _switch = false;

    bool qRSpot = false;

    float timer = 0.0f;

    // FPS設定 カメラのインスタンスに使う
    const int FPS = 60;

    // カメラの回転
    const int ROTATION = -90;

    // 最大画面幅、高さ
    const int SCREEN_WIDTH = 1280;
    const int SCREEN_HEIGHT = 720;

    // 画面サイズ
    const int SCREEN_SIZE = 2;

    [SerializeField]
    RawImage cameraImage;

    ActiveChange activeChange;
    //=======================================================================================
    //! @brief 開始処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return 反復処理をサポートするインターフェース
    //=======================================================================================
    IEnumerator Start()
    {
        activeChange = GetComponent<ActiveChange>();

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
        ////Quadを画面いっぱいに広げる
        //float _h = mainCamera.orthographicSize * 2;
        //float _w = _h * mainCamera.aspect;
        //// スマホが横ならそのまま
        //if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        //{
        //    transform.localScale = new Vector3(_h, _w, 1);
        //}
        //// 縦なら回転させる
        //if (Input.deviceOrientation == DeviceOrientation.FaceUp)
        //{
        //    transform.localScale = new Vector3(_h, _w, 1);
        //    transform.localRotation *= Quaternion.Euler(0, 0, ROTATION);
        //}
        // ====================================================================================

        if (devices.Length > 0)
        {
            // デバイスの情報[0]を格納
            WebCamDevice cam = devices[0];

            // カメラに映っているテクスチャを追加する
            WebCamTexture _wCam = new WebCamTexture(cam.name);

            //// rawImageに映っているテクスチャを張り付ける
            cameraImage.texture = _webCamTex;

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


            _webCamTex = new WebCamTexture(cam.name, Screen.width, Screen.height, FPS);
            _wCam.Stop();


            _webCamTex = new WebCamTexture();
            cameraImage.texture = _webCamTex;
            _webCamTex.Play();
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
        if(_webCamTex != null)
        {
            if (qRSpot)
            {
                // カメラのスイッチ ON
                _switch = true;
                // カメラの起動
                _webCamTex.Play();
                // RawImageを表示する
                activeChange.CameraImage.SetActive(true);
            }
            else
            {
                // カメラの終了
                _webCamTex.Stop();
                // カメラのスイッチ OFF
                _switch = false;
                // RawImageを非表示にする
                activeChange.CameraImage.SetActive(false);
            }
        }

        // 映像のテクスチャがあるか
        if (_webCamTex != null)
        {
            // カメラが起動していた場合
            if (_switch != false)
            {
                // 読み込んだ情報を格納
                _result = QRCodeHelper.Read(_webCamTex);
                if (_result != "error")
                {
                    // Spotの起動フラグをOffにする
                    qRSpot = false;
                    // カメラの終了
                    _webCamTex.Stop();
                    // カメラのスイッチ OFF
                    _switch = false;

                    activeChange.Text.SetActive(true);
                }
                // 読み込んで格納した文字、数値をデバッグ表示
                Debug.LogFormat("result : " + _result);
                Debug.LogFormat(_result);
            }
        }

        if(activeChange.Text.activeInHierarchy)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if(timer > 5.0f)
            {
                activeChange.Text.SetActive(false);
                timer = 0.0f;
            }
        }
    }

    public string Result
    {
        get { return _result; }
        set { _result = value; }
    }

    public bool QRSpot
    {
        get { return qRSpot; }
        set { qRSpot = value; }
    }

}


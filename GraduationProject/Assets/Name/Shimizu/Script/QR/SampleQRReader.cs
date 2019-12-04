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
    WebCamTexture _webCamTex = default;

    // 読み込んだ結果格納
    private string _result = null;

    // カメラの On Off
    bool _switch = false;

    // QRSpotに触れたかの真偽
    bool qRSpot = false;

    // 時間を図る
    float timer = 0.0f;

    // FPS設定 カメラのインスタンスに使う
    const int FPS = 60;

    // カメラの回転
    const int ROTATION = 180;

    // 最大画面幅、高さ
    const int SCREEN_WIDTH = 1920;
    const int SCREEN_HEIGHT = 1200;

    // 画面サイズ
    const int SCREEN_SIZE = 2;

    //イベント管理クラス
    [SerializeField]
    private EventDirector _event = default;

    // 映ったテクスチャを貼る
    [SerializeField]
    RawImage rawImage = default;

    // Activeの変更
    ActiveChange activeChange = default;

    QRReadID qRReadID = default;

    // プレイヤーのRigidbody
    Rigidbody rgb;

    // 変換先
    int num = -1;

    //=======================================================================================
    //! @brief 開始処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return 反復処理をサポートするインターフェース
    //=======================================================================================
    IEnumerator Start()
    {
        // ActiveChangeにアクセス
        activeChange = GetComponent<ActiveChange>();

        qRReadID = GetComponent<QRReadID>();

        // プレイヤーのRigidbody
        rgb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        // カメラを使用する際に許可を求める
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        // カメラの許可を行っているか
        if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        {
            yield break;
        }

        // 利用可能なカメラのデバイス
        WebCamDevice[] devices = WebCamTexture.devices;

        // デバイスの情報がnullじゃないか確認
        if (devices == null || devices.Length == 0)
        {
            yield break;
        }

        // スマホ内での処理====================================================================

        if (Application.platform == RuntimePlatform.Android)
        {
            rawImage.rectTransform.localScale = new Vector3(1, -1, 1);
        }

        if (devices.Length > 0)
        {
            // デバイスの情報[0]を格納
            WebCamDevice cam = devices[0];
            // カメラに映っているテクスチャを追加する
            WebCamTexture _wCam = new WebCamTexture(cam.name);

            // rawImageに映っているテクスチャを張り付ける
            rawImage.texture = _webCamTex;

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

            // 実態を作成する
            _webCamTex = new WebCamTexture(cam.name, Screen.width, Screen.height, FPS);
            // カメラを止める
            _wCam.Stop();

            // カメラに映っているテクスチャを追加する
            _webCamTex = new WebCamTexture();
            // テクスチャを張りつける
            rawImage.texture = _webCamTex;
            // カメラの起動
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
        // webカメラの起動,終了処理==========================================================
        // nullチェック
        if (_webCamTex != null)
        {
            // QRSpotがtrueか(QRSpotに触れた時にtrueになる)
            if (qRSpot)
            {
                // カメラのスイッチ ON
                _switch = true;               
                // RawImageを表示する
                activeChange.CameraPanel.SetActive(true);
                // カメラの起動
                _webCamTex.Play();
            }
            else
            {
                // カメラの終了
                _webCamTex.Stop();
                // カメラのスイッチ OFF
                _switch = false;
                // RawImageを非表示にする
                activeChange.CameraPanel.SetActive(false);
            }
        }
        // ==================================================================================

        // 向きを取得してRawImageを回転
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            // タブレットの向きに合わせて、画面の向きを変える
            rawImage.rectTransform.rotation = Quaternion.Euler(0, ROTATION, ROTATION);
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            // タブレットの向きに合わせて、画面の向きを変える
            rawImage.rectTransform.rotation = Quaternion.Euler(0, ROTATION, 0);
        }



        // QRの読み込み,テキストの表示処理===================================================
        // 映像のテクスチャがあるか
        if (_webCamTex != null)
        {
            // カメラが起動していた場合
            if (_switch != false)
            {
                // 読み込んだ情報を格納
                _result = QRCodeHelper.Read(_webCamTex);
                // _resultにerror意外だった場合
                if (_result != "error")
                {
                    // Spotの起動フラグをOffにする
                    qRSpot = false;
                    // カメラの終了
                    _webCamTex.Stop();
                    // カメラのスイッチ OFF
                    _switch = false;
                    // テキストPanelの表示
                    activeChange.TextPanel.SetActive(true);

                    int.TryParse(_result, out num);
                }
            }
        }

        // ==================================================================================


        // テキストの非表示処理==============================================================
        // テキストPanelが表示されているかどうか
        if (activeChange.TextPanel.activeInHierarchy)
        {
            // 秒数を数える
            timer += Time.deltaTime;
            // 5秒たったら
            if(timer > 5.0f)
            {
                // テキストPanelを非表示にする
                activeChange.TextPanel.SetActive(false);

                // イベントを空にする
                _event.IsEventKIND = EventDirector.EventKIND.NONE;

                // タイマーをリセットする
                timer = 0.0f;

                // プレイヤーのRigidBodyをフリーズ解除
                rgb.constraints = RigidbodyConstraints.None;

                // もし正規のQRコードじゃなかった場合
                if (qRReadID.Num == 0)
                {
                    // もう一度QRモードにする
                    _event.IsEventKIND = EventDirector.EventKIND.RULE_QR;
                    // カメラの起動
                    qRSpot = true;
                }
            }
        }
        // ==================================================================================
    }

    //=======================================================================================
    //! @brief 結果の取得設定
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public string Result { get { return _result; } set { _result = value; } }

    //=======================================================================================
    //! @brief QRSpotの取得関数
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public bool QRSpot { get { return qRSpot; } set { qRSpot = value; } }
}
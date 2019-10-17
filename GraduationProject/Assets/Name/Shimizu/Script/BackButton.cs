//=======================================================================================
//! @file   BackButton
//! @brief  ボタンをタッチした時の処理
//! @author 志水大輔
//! @date   10/9
//! @note   書き換える可能性あり
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    Button backButton = default;

    //イベント管理クラス
    [SerializeField]
    private EventDirector _event = default;

    // QRDirectorの変数
    GameObject qRDirector;

    // QRSpotの判定変数
    SampleQRReader spot;

    float timer = 0.0f;

    bool pushFlag = false;
    //=======================================================================================
    //! @brief 開始処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void Start()
    {
        // QRDirectorのオブジェクト参照
        qRDirector = GameObject.FindWithTag("QRDirector");

        // SampleQRReaderにアクセス
        spot = qRDirector.gameObject.GetComponent<SampleQRReader>();
    }

    void Update()
    {
        // Flagがtrueだった場合に
        if(pushFlag)
        {
            // 秒数を数える
            timer += Time.deltaTime;
        }

        // 5秒たったら
        if (timer > 1.0f)
        {
            // イベントを空にする
            _event.IsEventKIND = EventDirector.EventKIND.NONE;

            // Flagをfalseにする
            pushFlag = false;

            // タイマーを0に戻す
            timer = 0.0f;

            // カメラの終了
            spot.QRSpot = false;
        }
    }

    //=======================================================================================
    //! @brief タッチした時の処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public void PointerDown()
    {
        pushFlag = true;
        // 小さくする
        backButton.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    //=======================================================================================
    //! @brief 放した時の処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public void PointerUp()
    {
        // 元の大きさに戻す
        backButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

}

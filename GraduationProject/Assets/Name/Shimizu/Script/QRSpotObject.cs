//=======================================================================================
//! @file   QRSpotObject
//! @brief  QRSpotに触れた時の処理
//! @author 志水大輔
//! @date   10/9
//! @note   書き換える可能性あり
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRSpotObject : MonoBehaviour
{
    // QRDirectorの変数
    GameObject qRDirector;

    // QRSpotの変数
    SampleQRReader spot;

    //=======================================================================================
    //! @brief 開始処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void Start()
    {
        // QRDirectorを格納
        qRDirector = GameObject.FindWithTag("QRDirector");
        // SampleQRReaderを格納
        spot = qRDirector.gameObject.GetComponent<SampleQRReader>();
    }

    //=======================================================================================
    //! @brief 当たった時の処理
    //! @param[in] 当たり判定
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void OnTriggerEnter(Collider col)
    {
        // Playerに当たったら
        if(col.gameObject.tag == "Player")
        {
            // カメラをオンにするフラグをtrueにする
            spot.QRSpot = true;
        }
    }
}

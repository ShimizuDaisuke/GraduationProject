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

public class BackButton : MonoBehaviour
{
    // QRDirectorの変数
    GameObject qRDirector;

    // QRSpotの判定変数
    SampleQRReader spot;

    //=======================================================================================
    //! @brief 開始処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void Start()
    {
        // QRDirectorの格納
        qRDirector = GameObject.FindWithTag("QRDirector");

        // SampleQRReaderの格納
        spot = qRDirector.gameObject.GetComponent<SampleQRReader>();
    }

    //=======================================================================================
    //! @brief タッチした時の処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public void OnClick()
    {
        // カメラの終了
        spot.QRSpot = false;
    }
}

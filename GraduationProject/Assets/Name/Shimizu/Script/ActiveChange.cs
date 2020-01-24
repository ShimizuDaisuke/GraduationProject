//=======================================================================================
//! @file   ActiveChange
//! @brief  Activeの管理
//! @author 志水大輔
//! @date   10/9
//! @note   書き換える可能性あり
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChange : MonoBehaviour
{
    // カメラPanelの変数
    [SerializeField]
    GameObject cameraPanel;

    // textPanelの変数
    [SerializeField]
    GameObject qRImage;

    //=======================================================================================
    //! @brief カメラパネルの取得設定
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public GameObject CameraPanel {get { return cameraPanel; }set { cameraPanel = value; }}

    //=======================================================================================
    //! @brief テキストパネルの取得設定
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public GameObject QRImage { get { return qRImage; }set { qRImage = value; } }
}

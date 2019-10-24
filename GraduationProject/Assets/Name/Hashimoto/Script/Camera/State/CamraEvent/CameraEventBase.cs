// -----------------------------------------------------------------------------------------
//! @file       CameraEventBase.cs
//!
//! @brief      イベント用のカメラの動きの元
//!
//! @author     橋本 奉武
//!
//! @date       2019.10.14
// -----------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEventBase : MonoBehaviour
{
    // プレイヤー
    private GameObject Player;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // プレイヤーを探す
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    /// <param name="camera3D">3D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    /// <param name="camera2D">2D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    virtual public void MoveCameraByEvent(ref GameObject camera3D, ref GameObject camera2D)
    {
        // 何もなし
    }
    
}

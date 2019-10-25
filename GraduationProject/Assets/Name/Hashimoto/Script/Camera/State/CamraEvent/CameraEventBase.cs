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
    protected GameObject Player;

    // 2D空間上に映すカメラ
    protected GameObject Camera2D;

    // 3D空間上に映すカメラ
    protected GameObject Camera3D;


    /// <summary>
    /// 開始処理
    /// </summary>
    protected void Initilaize()
    {
        // プレイヤーを探す
        Player = GameObject.FindGameObjectWithTag("Player");

        // 2D空間上に映すカメラを探す
        Camera2D = GameObject.FindGameObjectWithTag("Camera2D");

        // 3D空間上に映すカメラ
        Camera3D = GameObject.FindGameObjectWithTag("Camera3D");

    }

    /// <summary>
    /// イベント用にカメラを動かす
    /// </summary>
    virtual public void MoveCameraByEvent()
    {
        // 何もなし
    }
    
}

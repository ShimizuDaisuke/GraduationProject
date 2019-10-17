// -----------------------------------------------------------------------------------------
//! @file       CameraEvent.cs
//!
//! @brief      イベント用のカメラの動き
//!
//! @author     橋本 奉武
//!
//! @date       2019.10.14
// -----------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 型名省略
/// </summary>

// イベントの種類
using EventKind = EventDirector.EventKIND;

public class CameraEvent : MonoBehaviour
{
    /// <summary>
    /// イベント用のカメラの更新処理
    /// </summary>
    /// <param name="eventkind">イベントの種類(処理上変えられない)</param>
    /// <param name="camera3D">3D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    /// <param name="camera2D">2D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    public void EventUpdate(EventKind eventkind, ref GameObject camera3D ,ref GameObject camera2D)
    {
        // イベントの種類によって、カメラの動きを変える
        switch(eventkind)
        {
            
        }
    }

}

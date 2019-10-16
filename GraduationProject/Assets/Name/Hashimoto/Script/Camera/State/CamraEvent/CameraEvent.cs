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

using EventKind = EventDirector.EventKIND;

public class CameraEvent : MonoBehaviour
{
    /// <summary>
    /// イベント用のカメラの更新処理
    /// </summary>
    /// <param name="eventkind">イベントの種類</param>
    public void EventUpdate(EventKind eventkind)
    {
        // イベントの種類によって、カメラの動きを変える
        switch(eventkind)
        {
            
        }
    }

}

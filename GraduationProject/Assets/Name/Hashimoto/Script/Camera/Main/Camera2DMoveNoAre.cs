// -----------------------------------------------------------------------------------------
//! @file       Camera2DMoveNoAre.cs
//!
//! @brief      カメラが2Dの場合、プレイヤーが通れない範囲
//!
//! @author     橋本 奉武
//!
//! @date       2019.11.14
//!
//! @note     後ほど、書き換える
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// イベントの種類
using EventKind = EventDirector.EventKIND;

public class Camera2DMoveNoAre : MonoBehaviour
{
    // カメラの監督
    [SerializeField]
    private GameObject CameraDirectarObj;

    // イベントの監督
    [SerializeField]
    private GameObject EventDirectarObj;

    // カメラが2Dの場合、プレイヤーが通れない範囲
    [SerializeField]
    private GameObject obj;

    // スクリプト：カメラの監督
    private CameraDirector Script_CameraDirector;

    // スクリプト：イベントの監督
    private EventDirector Script_EventDirector;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // スクリプト：カメラの監督  取得
        Script_CameraDirector = CameraDirectarObj.GetComponent<CameraDirector>();

        // スクリプト：イベントの監督 取得
        Script_EventDirector = EventDirectarObj.GetComponent<EventDirector>();
    } 

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // <テスト>

        // カメラが2Dの場合 もしくは  イベントが発生中
        if ((Script_CameraDirector.IsAppearCamera3D) || (Script_EventDirector.IsEventKIND != EventKind.NONE))
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }

    }
}

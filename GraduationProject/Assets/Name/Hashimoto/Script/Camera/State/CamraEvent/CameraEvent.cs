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

    // スクリプト：イベント用のカメラ
    private CameraEventBase[] Script_CameraEventBase;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // イベント用のカメラ を作成する
        Script_CameraEventBase = new CameraEventBase[(int)EventKind.MAX];

        // --------------------------------------------------------------------------------------------------
        // ※ 下記の行数をずらさない!

        // 「プレイヤーが定規によって投げれた」イベント
        Script_CameraEventBase[(int)EventKind.RULE_MOVE_STRAIGHT] = GetComponent<EventCameraRuleStraight> ();

        // 「プレイヤーが定規によって直進移動する」イベント
        Script_CameraEventBase[(int)EventKind.RULE_THOW] = GetComponent<EventCameraRuleThow>();

        // 「プレイヤーが薄い本をドミノ倒しする」イベント
        Script_CameraEventBase[(int)EventKind.RULE_DOMINO] = GetComponent<EventCameraDomino>();

        // 「ノートの落書きを消す」イベント


        // 「プレイヤーがカッターナイフをしまう」イベント
        Script_CameraEventBase[(int)EventKind.RULE_CUTTERKNIFE] = GetComponent<EventCameraCutterKnife>();

        // 「ハサミ切る」イベント
        Script_CameraEventBase[(int)EventKind.SCISSORS_CUT] = GetComponent<EventCameraScissorsCut>();

    }

    /// <summary>
    /// イベント用のカメラの更新処理
    /// </summary>
    /// <param name="eventkind">イベントの種類(処理上変えられない)</param>
    /// <param name="camera3D">3D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    /// <param name="camera2D">2D空間上に映すカメラ(ref:位置や回転などを変えられる)</param>
    public void EventUpdate(EventKind eventkind, ref GameObject camera3D ,ref GameObject camera2D)
    {
        // そのイベントのカメラの動きがない場合は、何もしない
        if (Script_CameraEventBase[(int)eventkind] == null) return;

        // イベントの種類によって、カメラの動きを変える
        Script_CameraEventBase[(int)eventkind].MoveCameraByEvent();
    }
}
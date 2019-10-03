// -----------------------------------------------------------------------------------------
//! @file       CameraDirector.cs
//!
//! @brief      カメラの監督
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.26
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{
    // カメラの状態
    enum CameraState
    {
        ERR = -1,           // 例外
        FOLLOWPLAYER,       // カメラがプレイヤーに追従する
        MOVE2D3D,           // カメラが2D⇔3Dへ動く
        NOMOVE              // カメラを動かさない
    }

    // 2Dカメラ
    [SerializeField] private GameObject Camera2D = default;

    // 3Dカメラ
    [SerializeField] private GameObject Camera3D = default;

    // 「2Dカメラから3Dカメラへ」移動用のカメラ
    [SerializeField] private GameObject MoveFrom2DTo3DCamera = default;

    //「3Dカメラから2Dカメラへ」移動用のカメラ
    [SerializeField] private GameObject MoveFrom3DTo2DCamera = default;

    // 3Dカメラを表示するか(false：2Dカメラで表示している / true：3Dカメラで表示している)
    private bool IsNowChange3DCamera = true;

    // 2Dカメラと3Dカメラの間へ移動しているか
    private bool IsNowMove2DCamera3DCamera = false;

    // 以前「3Dカメラから2Dカメラ」もしくは「2Dカメラから3Dカメラ」へ移動したか
    private bool IsOnceMove2DCamera3DCamera = false;

    // スクリプト : 2Dカメラ ↔ 3Dカメラへ動く
    private CameraMove2D3D Script_CameraMove2D3D;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // 以前移動の状態の設定
        IsOnceMove2DCamera3DCamera = IsNowMove2DCamera3DCamera;

        // 2Dカメラの表示状態
        Camera2D.SetActive(!IsNowChange3DCamera);

        // 3Dカメラの表示状態
        Camera3D.SetActive(IsNowChange3DCamera);

        // 「2Dカメラから3Dカメラへ」移動用のカメラを非表示する
        MoveFrom2DTo3DCamera.SetActive(false);

        //「3Dカメラから2Dカメラへ」移動用のカメラを非表示する
        MoveFrom3DTo2DCamera.SetActive(false);

        // スクリプト: 2Dカメラ ↔ 3Dカメラへ動く の設定
        Script_CameraMove2D3D = GetComponent<CameraMove2D3D>();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // <テスト>----------------------------------------------------------------

        // スペースキーを押されたらカメラを切り替える
        if ((Input.GetKeyDown(KeyCode.Space)) && (IsNowMove2DCamera3DCamera == false))
        {
            // 2D ↔ 3Dカメラに切り替える
            IsNowChange3DCamera = !IsNowChange3DCamera;

            // 「2Dカメラと3Dカメラの間へ移動する準備を行う
            IsNowMove2DCamera3DCamera = true;
        }

        // -------------------------------------------------------------------------

        // 2Dカメラと3Dカメラの間へ移動する場合
        if (IsNowMove2DCamera3DCamera == true)
        {
            // 最終的に3Dカメラに映す場合
            if (IsNowChange3DCamera == true)
            {
                // 2Dカメラから3Dカメラの位置へ移動する
                Script_CameraMove2D3D.MoveMiddle2D3DCameraPos(MoveFrom2DTo3DCamera, Camera2D, Camera3D,ref IsNowMove2DCamera3DCamera,IsOnceMove2DCamera3DCamera,  IsNowChange3DCamera);
            }
            // 最終的に2Dカメラに映す場合
            else
            {
                // 3Dカメラから2Dカメラの位置へ移動する
                Script_CameraMove2D3D.MoveMiddle2D3DCameraPos(MoveFrom3DTo2DCamera, Camera3D, Camera2D,ref IsNowMove2DCamera3DCamera, IsOnceMove2DCamera3DCamera,  IsNowChange3DCamera);
            }
        }

        // 常に以前移動の状態を設定する
        IsOnceMove2DCamera3DCamera = IsNowMove2DCamera3DCamera;
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

        //  2Dカメラと3Dカメラの間へ移動しているか
        public bool IsMove2D3DCameraPos { get { return IsNowMove2DCamera3DCamera; } set { IsNowMove2DCamera3DCamera = value; } }

        // 3Dカメラを表示しているか(false：2Dカメラ表示 / true：3Dカメラ表示)
        public bool IsAppearCamera3D { get { return IsNowChange3DCamera; } private set { IsNowChange3DCamera = value; } }
}

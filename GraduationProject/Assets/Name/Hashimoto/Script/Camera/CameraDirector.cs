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
    // 2Dカメラ
    [SerializeField]
    private GameObject Camera2D = default;

    // 3Dカメラ
    [SerializeField]
    private GameObject Camera3D = default;

    // 「3Dカメラから2Dカメラへ」移動用のカメラ
    [SerializeField]
    private GameObject MoveFrom3DTo2DCamera = default;

    //「2Dカメラから3Dカメラへ」移動用のカメラ
    [SerializeField]
    private GameObject MoveFrom2DTo3DCamera = default;

    // 移動時間
    [SerializeField]
    private float Speed_Move2DCamera3DCamera = 0.5f;

    // -----------------------------------------------------------------------------------------

    // 移動速度
    private Vector3 Velocity;

    // 回転速度
    private Vector3 RotatingSpeed;

    // -----------------------------------------------------------------------------------------

    // 初めて「3Dカメラから2Dカメラへ」「2Dカメラから3Dカメラへ」移動して経過した時間
    private float MoveTime = 0.0f;

    // -----------------------------------------------------------------------------------------

    // 2Dカメラと3Dカメラの間へ移動しているか
    private bool IsNowMove2DCamera3DCamera = false;

    // 以前「3Dカメラから2Dカメラ」もしくは「2Dカメラから3Dカメラ」へ移動したか
    private bool IsOnceMove2DCamera3DCamera = false;

    // -----------------------------------------------------------------------------------------

    // 3Dカメラを表示するか(false：2Dカメラで表示している / true：3Dカメラで表示している)
    private bool IsNowChange3DCamera = true;

    // スクリプト :「2Dカメラのみ」もしくは「3Dカメラのみ」に表示されるオブジェクト
    private CameraAppearDisAppearObject Script_AppearDisAppearObjByCamera;

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

        // 「3Dカメラから2Dカメラへ」移動用のカメラを非表示する
        MoveFrom3DTo2DCamera.SetActive(false);

        //「2Dカメラから3Dカメラへ」移動用のカメラを非表示する
        MoveFrom2DTo3DCamera.SetActive(false);

        // 「2Dカメラのみ」もしくは「3Dカメラのみ」に表示されるオブジェクトの設定
        Script_AppearDisAppearObjByCamera = GetComponent<CameraAppearDisAppearObject>();

        // 2Dや3Dカメラのみ表示されるオブジェクトを表示もしくは非表示させる
        Script_AppearDisAppearObjByCamera.ChangeObjByCamera(IsNowChange3DCamera);
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
                MoveMiddle2D3DCameraPos(MoveFrom2DTo3DCamera, Camera2D, Camera3D);
            }
            // 最終的に2Dカメラに映す場合
            else
            {
                // 3Dカメラから2Dカメラの位置へ移動する
                MoveMiddle2D3DCameraPos(MoveFrom3DTo2DCamera, Camera3D, Camera2D);
            }
        }

        // 常に以前移動の状態を設定する
        IsOnceMove2DCamera3DCamera = IsNowMove2DCamera3DCamera;
    }

    /// <summary>
    /// 2Dカメラと3Dカメラの間による移動処理
    /// </summary>
    /// <param name="maincamera">移動用のカメラ</param>
    /// <param name="startcamera">開始位置にいるカメラ</param>
    /// <param name="endcamera">終了位置にいるカメラ</param>
    void MoveMiddle2D3DCameraPos(GameObject maincamera, GameObject startcamera, GameObject endcamera)
    {
        // 初めて「3Dカメラから2Dカメラへ」「2Dカメラから3Dカメラへ」移動する場合
        if (IsOnceMove2DCamera3DCamera == false)
        {
            // 開始位置にいるカメラを非表示する
            startcamera.SetActive(false);

            // 終了位置にいるカメラを非表示する
            endcamera.SetActive(false);

            // 移動用のカメラを表示する
            maincamera.SetActive(true);

            // ----------------------------------------------------------------------------------------------

            // 3Dカメラと2Dカメラの距離
            Vector3 direction = endcamera.transform.position - startcamera.transform.position;

            //  移動速度を計算する
            Velocity = direction / Speed_Move2DCamera3DCamera;

            // ----------------------------------------------------------------------------------------------

            // 3Dカメラと2Dカメラの回転差
            Vector3 rotate = endcamera.transform.localEulerAngles - startcamera.transform.localEulerAngles;

            // 回転速度を計算する
            RotatingSpeed = rotate / Speed_Move2DCamera3DCamera;

            // ----------------------------------------------------------------------------------------------

            // 「3Dカメラから2Dカメラへ」「2Dカメラから3Dカメラへ」移動用のカメラを初期化する
            maincamera.transform.position = startcamera.transform.position;                           // 位置
            maincamera.transform.rotation = Quaternion.Euler(startcamera.transform.localEulerAngles); // 回転

            // ----------------------------------------------------------------------------------------------

            // 2Dや3Dカメラのみ表示されるオブジェクトを表示非表示させる
            Script_AppearDisAppearObjByCamera.ChangeObjByCamera(IsNowChange3DCamera);

        }

        // 初めて「3Dカメラから2Dカメラへ」「2Dカメラから3Dカメラへ」移動して経過した時間を計る
        MoveTime += Time.deltaTime;

        // カメラの移動時間が超えた場合
        if (MoveTime >= Speed_Move2DCamera3DCamera)
        {
            // 初めて「3Dカメラから2Dカメラへ」「2Dカメラから3Dカメラへ」移動して経過した時間をリセットする
            MoveTime = 0.0f;

            // 2Dカメラと3Dカメラの間へ移動しないようにする
            IsNowMove2DCamera3DCamera = false;

            // 移動用のカメラを非表示する
            maincamera.SetActive(false);

            // 終了位置にいるカメラを表示する
            endcamera.SetActive(true);
        }
        else
        // カメラの移動時間が超えていない場合
        {
            // メインとなるカメラの位置を動かす
            maincamera.transform.position = startcamera.transform.position + Velocity * MoveTime;
            // メインとなるカメラを回転する
            maincamera.transform.rotation = Quaternion.Euler(startcamera.transform.localEulerAngles + RotatingSpeed * MoveTime);
        }
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

    //  2Dカメラと3Dカメラの間へ移動しているか
    public bool IsMove2D3DCameraPos { get { return IsNowMove2DCamera3DCamera; } set { IsNowMove2DCamera3DCamera = value; } }

    // 3Dカメラを表示しているか(false：2Dカメラ表示 / true：3Dカメラ表示)
    public bool IsAppearCamera3D { get { return IsNowChange3DCamera; } private set { IsNowChange3DCamera = value; } }
}

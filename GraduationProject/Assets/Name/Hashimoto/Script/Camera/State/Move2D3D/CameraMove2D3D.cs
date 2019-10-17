// -----------------------------------------------------------------------------------------
//! @file       CameraMove2D3D.cs
//!
//! @brief      2Dカメラ ↔ 3Dカメラへ動く
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.26
// -----------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 型名を省略する
using CameraState = CameraDirector.CameraState;     // カメラの状態

public class CameraMove2D3D : MonoBehaviour
{

    // 移動時間
    [SerializeField]
    private float Speed_Move2DCamera3DCamera = 0.5f;

    // プレイヤー
    private GameObject Player;

    // 移動速度
    private Vector3 Velocity;

    // 回転速度
    private Vector3 RotatingSpeed;

    // 初めて「3Dカメラから2Dカメラへ」「2Dカメラから3Dカメラへ」移動して経過した時間
    private float MoveTime = 0.0f;

    // スクリプト :「2Dカメラのみ」もしくは「3Dカメラのみ」に表示されるオブジェクト
    private CameraAppearDisAppearObject Script_AppearDisAppearObjByCamera;

    // スクリプト : 2Dカメラ⇔3Dカメラへ移動時にいるプレイヤーの位置
    private PlayerPosByCamera2D3D Script_PlayerPosByCamera2D3D;

    // 外部のスクリプト変数：3Dカメラを表示するか(false：2Dカメラで表示している / true：3Dカメラで表示している)
    private bool IsNowChange3DCamera = true;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // プレイヤーを探す
        Player = GameObject.FindGameObjectWithTag("Player");

        // 外部のスクリプト変数：3Dカメラを表示するか(false：2Dカメラで表示している / true：3Dカメラで表示している)の設定
        IsNowChange3DCamera = GetComponent<CameraDirector>().IsAppearCamera3D;

        // スクリプト : 「2Dカメラのみ」もしくは「3Dカメラのみ」に表示されるオブジェクトの設定
        Script_AppearDisAppearObjByCamera = GetComponent<CameraAppearDisAppearObject>();

        // スクリプト : 2Dカメラ⇔3Dカメラへ移動時にいるプレイヤーの位置の設定
        Script_PlayerPosByCamera2D3D = Player.GetComponent<PlayerPosByCamera2D3D>();

        // 2Dや3Dカメラのみ表示されるオブジェクトを表示もしくは非表示させる
        Script_AppearDisAppearObjByCamera.ChangeObjByCamera(IsNowChange3DCamera);
    }

    /// <summary>
    /// 2Dカメラと3Dカメラの間による移動処理
    /// </summary>
    /// <param name="maincamera">移動用のカメラ</param>
    /// <param name="startcamera">開始位置にいるカメラ</param>
    /// <param name="endcamera">終了位置にいるカメラ</param>
    /// <param name="nowstate">カメラの今の状態</param>
    /// <param name="isdifferstatenowonce">カメラの状態が今と前で異なっているか</param>
    /// <param name="iscamera3d">最終的に3Dカメラになるのか</param>
    public void MoveMiddle2D3DCameraPos(GameObject maincamera, GameObject startcamera, GameObject endcamera,ref CameraState nowstate,ref bool isdifferstatenowonce, bool iscamera3d)
    {
        // 初めて「3Dカメラから2Dカメラへ」「2Dカメラから3Dカメラへ」移動する場合
        if (isdifferstatenowonce == true)
        {
            // カメラの今と前の状態を同じにする
            isdifferstatenowonce = false;

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
            maincamera.transform.localPosition = startcamera.transform.localPosition;                           // 位置
            maincamera.transform.rotation = Quaternion.Euler(startcamera.transform.localEulerAngles);           // 回転

            // ----------------------------------------------------------------------------------------------

            // 2Dや3Dカメラのみ表示されるオブジェクトを表示非表示させる
            Script_AppearDisAppearObjByCamera.ChangeObjByCamera(iscamera3d);

            // カメラが移動する前に、プレイヤーの位置を記憶する
            Script_PlayerPosByCamera2D3D.PlayerOncePos = Player.transform.position;
        }

        // 初めて「3Dカメラから2Dカメラへ」「2Dカメラから3Dカメラへ」移動して経過した時間を計る
        MoveTime += Time.deltaTime;

        // カメラの移動時間が超えた場合
        if (MoveTime >= Speed_Move2DCamera3DCamera)
        {
            // カメラの動きを止める
            nowstate = CameraState.STOP;

            // カメラの今と前の状態が異なる
            isdifferstatenowonce = true;

            // 初めて「3Dカメラから2Dカメラへ」「2Dカメラから3Dカメラへ」移動して経過した時間をリセットする
            MoveTime = 0.0f;

            // 移動用のカメラを非表示する
            maincamera.SetActive(false);

            // 終了位置にいるカメラを表示する
            endcamera.SetActive(true);

            // それぞれのカメラが時間をかけずにプレイヤーに追従する(この処理がないと、カメラが少し動いてしまう)
            GetComponent<CameraFollowPlayer>().FllowPlayerNoSlowy();
        }
        else
        // カメラの移動時間が超えていない場合
        {
            // メインとなるカメラの位置を動かす
            maincamera.transform.localPosition = startcamera.transform.localPosition + Velocity * MoveTime;
            // メインとなるカメラを回転する
            maincamera.transform.rotation = Quaternion.Euler(startcamera.transform.localEulerAngles + RotatingSpeed * MoveTime);

            Debug.Log(endcamera.transform.localPosition);

            // プレイヤーの位置を維持する
            Script_PlayerPosByCamera2D3D.KeepPlayerPosByCameraMove2D3D();
        }
    }
}

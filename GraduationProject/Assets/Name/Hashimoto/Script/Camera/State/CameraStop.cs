// -----------------------------------------------------------------------------------------
//! @file       CameraStop.cs
//!
//! @brief      カメラの動きを止める
//!
//! @author     橋本 奉武
//!
//! @date       2019.9.26
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 型名を省略
using CameraState = CameraDirector.CameraState;     // カメラの状態

public class CameraStop : MonoBehaviour
{
    // 時間 : カメラの動きを止めてから、カメラを2D⇔3Dへ動かすまでの時間
    private float StopTime_NextMove2D3D = 1.0f;

    // 時間 :　カメラの動きを止めてから、カメラをプレイヤーに追従させるまでの時間
    private float StopTime_NextFollowPlayer = 0.5f;

    // 透明度 : 通常の透明度 
    private float NormalTransparence = 1.0f;

    // 透明度 : プレイヤーが地面以外に当たったオブジェクトの透明度
    private float PlayerHitObjTransparence = 0.2f;

    // --------------------------------------------------------------------------------------

    // プレイヤー
    private GameObject Player;
    
    // カメラの動きを止めた後の「カメラの状態」
    private CameraState NextStateAfterCameraStop = CameraState.ERR;

    // カメラの動きを止めてから経過した時間
    private float TimeAfterCameraStop = 0.0f;

    // スクリプト：2Dカメラ⇔3Dカメラへ移動時にいるプレイヤーの位置
    private PlayerPosByCamera2D3D Script_PlayerPosByCamera2D3D;

    // スクリプト：カメラの監督
    private CameraDirector Script_CameraDirector;


    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        // プレイヤーを探す
        Player = GameObject.FindGameObjectWithTag("Player");

        // スクリプト：2Dカメラ⇔3Dカメラへ移動時にいるプレイヤーの位置 の設定
        Script_PlayerPosByCamera2D3D = Player.GetComponent<PlayerPosByCamera2D3D>();

        // スクリプト：カメラの監督
        Script_CameraDirector = GetComponent<CameraDirector>();

    }

    /// <summary>
    /// カメラの動きを止める
    /// </summary>
    /// <param name="nowstate">カメラの今の状態</param>
    /// <param name="isdifferstatenowonce">カメラの今と前の状態が異なるか</param>
    public void Stop(ref CameraState nowstate,ref bool isdifferstatenowonce)
    {
        // 初めてカメラの動きを止めた場合
        if(isdifferstatenowonce == true)
        {
            // カメラの今と前の状態を同じにする
            isdifferstatenowonce = false;

            // プレイヤーが地面以外のオブジェクトに当たった場合
            if (Script_PlayerPosByCamera2D3D.IsHitPlayerNoGroundObj == true)
            {
                // カメラの動きを止めた後に、カメラの状態を2D⇔3Dに動くように設定する
                NextStateAfterCameraStop = CameraState.MOVE2D3D;

                // プレイヤーの地面以外のオブジェクトを透明にする
                Script_PlayerPosByCamera2D3D.MakeTransparencePlayerHitObjNoGround(PlayerHitObjTransparence);
            }
            else
            {
                // カメラの動きを止めた後に、カメラの状態をプレイヤーに追従するように設定する
                NextStateAfterCameraStop = CameraState.FOLLOWPLAYER;
            }
        }

        // カメラの動きを止めてから経過した時間を計る
        TimeAfterCameraStop += Time.deltaTime;

        // カメラの動きを止めた後の「カメラの状態」
        switch (NextStateAfterCameraStop)
        {
            // カメラの動きを止めた後に、カメラの状態をプレイヤーに追従するように設定した場合
            case CameraState.FOLLOWPLAYER:
            {
                //  時間 :　カメラの動きを止めてから、カメラをプレイヤーに追従させるまでの時間が経過した場合
                if (TimeAfterCameraStop >= StopTime_NextFollowPlayer)
                {
                    // 時間をリセットする
                    TimeAfterCameraStop = 0.0f;

                    // カメラの動きを止めた後の「カメラの状態」をリセットする
                    NextStateAfterCameraStop = CameraState.ERR;

                    // カメラがプレイヤーに追従するように設定する
                    Script_CameraDirector.State = CameraState.FOLLOWPLAYER;

                    // カメラの今と前の状態が異なる
                    isdifferstatenowonce = true;

                    // ---------------------------------------------------------------------------------

                    

                    // カメラが2D⇔3Dへ動かすのを終了させる
                    Script_CameraDirector.IsMove2D3DCameraPos = false;
                }

                break;
            }

            // カメラの動きを止めた後に、カメラの状態を2D⇔3Dに動くように設定した場合
            case CameraState.MOVE2D3D:
            {
                // カメラの動きを止めてから、カメラを2D⇔3Dへ動かすまでの時間が経過した場合
                if (TimeAfterCameraStop >= StopTime_NextMove2D3D)
                {
                    // 時間をリセットする
                    TimeAfterCameraStop = 0.0f;

                    // カメラの動きを止めた後の「カメラの状態」をリセットする
                    NextStateAfterCameraStop = CameraState.ERR;

                    // カメラが2D⇔3Dへ動くように設定する
                    Script_CameraDirector.State = CameraState.MOVE2D3D;

                    // カメラの今と前の状態が異なる
                    isdifferstatenowonce = true;

                    // ---------------------------------------------------------------------------------

                    // 2D ↔ 3Dカメラに切り替える
                    Script_CameraDirector.IsAppearCamera3D = !Script_CameraDirector.IsAppearCamera3D;

                    // プレイヤーの地面以外のオブジェクトの透明度を元に戻す
                    Script_PlayerPosByCamera2D3D.MakeTransparencePlayerHitObjNoGround(NormalTransparence);

                    // プレイヤーの地面以外のオブジェクトの当たり判定をリセットする
                    Script_PlayerPosByCamera2D3D.IsHitPlayerNoGroundObj = false;
                }

                break;
            }
        }

    }
}

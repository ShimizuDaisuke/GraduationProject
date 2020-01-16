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
    private float StopTime_NextMove2D3D = 1.5f;

    // 時間 :　カメラの動きを止めてから、カメラをプレイヤーに追従させるまでの時間
    private float StopTime_NextFollowPlayer = 0.3f;

    // 透明度 : 通常の透明度 
    private float NormalTransparence = 1.0f;

    // 透明度 : プレイヤーが地面以外に当たったオブジェクトの透明度
    private float PlayerHitObjTransparence = 0.2f;

    // 点滅時間の間隔
    private float FlashTimeGap = 0.4f;

    // プレイヤーから飛ばすレイの長さ
    private float RayDistance = 0.75f;

    // --------------------------------------------------------------------------------------

    // プレイヤー
    private GameObject PlayerObj;
    
    // カメラの動きを止めた後の「カメラの状態」
    private CameraState NextStateAfterCameraStop = CameraState.ERR;

    // カメラの動きを止めてから経過した時間
    private float TimeAfterCameraStop = 0.0f;

    // 点滅に関する時間
    private float FlashTime = 0.0f;

    // スクリプト：カメラの監督
    private CameraDirector Script_CameraDirector;

    // スクリプト：2Dカメラ⇔3Dカメラへ移動時にいるプレイヤーの位置
    private PlayerPosByCamera2D3D Script_PlayerPosByCamera2D3D;

    // スクリプト：カメラがプレイヤーに追従する
    private CameraFollowPlayer Script_CameraFollowPlayer;

    // スクリプト：カメラが2D⇔3Dへ切り替える前にプレイヤーの位置を決める処理
    private PlayerDeecidePosBeforeMoveCamera2D3D Script_PlayerDeecidePosBeforeMoveCamera2D3D;

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        // プレイヤーを探す
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

        // スクリプト：2Dカメラ⇔3Dカメラへ移動時にいるプレイヤーの位置 の設定
        Script_PlayerPosByCamera2D3D = PlayerObj.GetComponent<PlayerPosByCamera2D3D>();

        // スクリプト：カメラの監督 の設定
        Script_CameraDirector = GetComponent<CameraDirector>();

        // スクリプト：カメラがプレイヤーに追従する の設定
        Script_CameraFollowPlayer = GetComponent<CameraFollowPlayer>();

        // スクリプト：カメラが2D⇔3Dへ切り替える前にプレイヤーの位置を決める処理 の設定
        Script_PlayerDeecidePosBeforeMoveCamera2D3D = GetComponent<PlayerDeecidePosBeforeMoveCamera2D3D>();
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
                // --------------------------------------------------------------------------------------

                // プレイヤーの真下に1つの軸を中心に動かすオブジェクトの場合
                if(HitPlayer_ObjOneAxitsMove())
                {
                    // リセットする
                    Script_PlayerPosByCamera2D3D.IsHitPlayerNoGroundObj = false;

                    // カメラの動きを止めた後に、カメラの状態をプレイヤーに追従するように設定する
                    NextStateAfterCameraStop = CameraState.FOLLOWPLAYER;
                }
                else
                {
                    // カメラの動きを止めた後に、カメラの状態を2D⇔3Dに動くように設定する
                    NextStateAfterCameraStop = CameraState.MOVE2D3D;

                    // プレイヤーの地面以外のオブジェクトを透明にする
                    Script_PlayerPosByCamera2D3D.MakeTransparencePlayerHitObjNoGround(ref NormalTransparence, PlayerHitObjTransparence);
                }
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

                    // カメラの動きを止めた後の「カメラの次の状態」をリセットする
                    NextStateAfterCameraStop = CameraState.ERR;

                    // カメラがプレイヤーに追従するように設定する
                    Script_CameraDirector.State = CameraState.FOLLOWPLAYER;

                    // カメラの今と前の状態が異なる
                    isdifferstatenowonce = true;

                    // ---------------------------------------------------------------------------------
                 
                    // カメラが2D⇔3Dへ動かすのを終了させる
                    Script_CameraDirector.IsMove2D3DCameraPos = false;

                   // プレイヤーが地面以外のオブジェクトに当たった情報をリセットする(当たり判定による情報がtrueへ更新し続けたため)
                   Script_PlayerPosByCamera2D3D.IsHitPlayerNoGroundObj = false;

                    // プレイヤーの「Rigidbody」の位置と回転の固定(フリーズ)を解除する
                    PlayerObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                   // カメラが3D→2Dへ切り替えたときに、プレイヤーが入っていけいない領域を非表示させる
                   GetComponent<ObjStateByCameraMove2D3D>().ChangeActiveObjByCamera2DNoArea(false);

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

                    // カメラの動きを止めた後の「カメラの次の状態」をリセットする
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

                    // プレイヤーが地面以外に当たったオブジェクト
                    GameObject obj = Script_PlayerPosByCamera2D3D.PlayerHitObjNoGround;
                    // そのオブジェクトを表示させる
                    obj.SetActive(true);
                    // 点滅に関する時間をリセットする
                    FlashTime = 0.0f;

                    // 最終的にプレイヤーの位置を決める
                    Script_PlayerDeecidePosBeforeMoveCamera2D3D.DecidePlayerPosBeforeMoveCamera2D3D(Script_CameraDirector.IsAppearCamera3D,true);

                   // プレイヤーの位置が変わったため、カメラの位置もプレイヤーの位置に合わせて変える
                   Script_CameraFollowPlayer.FllowPlayerNoSlowy();

                }
                else
                // カメラを2D⇔3Dへ動かすまでの時間がまだ経過していない場合
                {
                    // 2D ↔ 3Dカメラに切り替える際にプレイヤーがいる位置を作成する
                    Script_PlayerPosByCamera2D3D.CreatePlayerPosByCameraMove2D3D(Script_PlayerPosByCamera2D3D.PlayerOncePos);

                        // 点滅に関する時間を計る
                        FlashTime += Time.deltaTime;

                        // オブジェクトの表示非表示を切り替える
                        if(FlashTime >= FlashTimeGap)
                        {
                            // 点滅に関する時間をリセットする
                            FlashTime += -FlashTimeGap;

                            // プレイヤーが地面以外に当たったオブジェクト
                            GameObject obj = Script_PlayerPosByCamera2D3D.PlayerHitObjNoGround;

                            // そのオブジェクトの状態
                            bool state = obj.activeInHierarchy;

                            // そのオブジェクトを表示非表示させる
                            obj.SetActive(!state);
                        }
                }

                    break;
            }
        }

    }

    /// <summary>
    /// プレイヤーの真下に1つの軸を中心に動かすオブジェクトなのか確認する
    /// </summary>
    /// <returns>そのオブジェクトは1つの軸を中心に動かすものか</returns>
    private bool HitPlayer_ObjOneAxitsMove()
    {
        // プレイヤーから飛ばすレイを作成する
        Ray ray =  new Ray(PlayerObj.transform.position, Vector3.down);

        // プレイヤーから飛ばしたレイに当たったオブジェクトの入れ物
        RaycastHit hit;

        // プレイヤーからレイを飛ばして何からのオブジェクトに衝突した場合
        if (Physics.Raycast(ray, out hit, RayDistance))
        {
            // プレイヤーが衝突したオブジェクトは軸中心に動かすオブジェクトの場合
            if (hit.transform.gameObject.GetComponent<Obj_OneAxitMove>() != null)
            {
                return true;
            }
        }

        return false;
    }
}

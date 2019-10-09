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
    public enum CameraState
    {
        ERR = -1,           // 例外
        FOLLOWPLAYER,       // プレイヤーに追従する
        MOVE2D3D,           // 2D ⇔ 3Dへ動く
        STOP,               // 動きを止める
    }

    // 2Dカメラ
    [SerializeField] private GameObject Camera2D = default;

    // 3Dカメラ
    [SerializeField] private GameObject Camera3D = default;

    // 「2Dカメラから3Dカメラへ」移動用のカメラ
    [SerializeField] private GameObject MoveFrom2DTo3DCamera = default;

    //「3Dカメラから2Dカメラへ」移動用のカメラ
    [SerializeField] private GameObject MoveFrom3DTo2DCamera = default;

    // プレイヤー
    private GameObject PlayerObj;

    // カメラの今の状態
    private CameraState NowState = CameraState.ERR;

    // カメラの今と前の状態が異なっているか(true:異なっている false:同じ)
    private bool IsDifferCameraStateNowOnce = false;

    // 3Dカメラを表示するか(false：2Dカメラで表示している / true：3Dカメラで表示している)
    private bool IsNowChange3DCamera = true;

    // 2Dカメラと3Dカメラの間へ移動しているか(カメラの状態 :「2D⇔3Dの動き」→「数ミリ秒止める」が含まれる)
    private bool IsMove2DCamera3DCamera = false;

    // スクリプト : 2Dカメラ ↔ 3Dカメラへ動く
    private CameraMove2D3D Script_CameraMove2D3D;

    // スクリプト : カメラの動きを止める
    private CameraStop Script_CameraStop;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // 2Dカメラの表示状態
        Camera2D.SetActive(!IsNowChange3DCamera);

        // 3Dカメラの表示状態
        Camera3D.SetActive(IsNowChange3DCamera);

        // 「2Dカメラから3Dカメラへ」移動用のカメラを非表示する
        MoveFrom2DTo3DCamera.SetActive(false);

        //「3Dカメラから2Dカメラへ」移動用のカメラを非表示する
        MoveFrom3DTo2DCamera.SetActive(false);

        // カメラがプレイヤーに追従するように設定する
        NowState  = CameraState.FOLLOWPLAYER;

        // プレイヤーを探す
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

        // スクリプト: 2Dカメラ ↔ 3Dカメラへ動く の設定
        Script_CameraMove2D3D = GetComponent<CameraMove2D3D>();

        // スクリプト：カメラの動きを止める　の設定
        Script_CameraStop = GetComponent<CameraStop>();
    }

    /// <summary>
    /// サブ更新処理
    /// </summary>
    void Update()
    {
        // <テスト>----------------------------------------------------------------

        // スペースキーを押されたらカメラを切り替える
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // カメラが2D⇔3Dへ切り替える準備を行う
            ChangeCamera2D3D();
        }

        // -------------------------------------------------------------------------
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void LateUpdate()
    {

        // カメラの状態
        switch (NowState)
        {
            // プレイヤーに追従する
            case CameraState.FOLLOWPLAYER:
            {
                break;
            }

            // カメラが2D ⇔ 3Dへ動く
            case CameraState.MOVE2D3D:
            {
                // 最終的に3Dカメラに映す場合
                if (IsNowChange3DCamera == true)
                {
                    // 2Dカメラから3Dカメラの位置へ移動する
                    Script_CameraMove2D3D.MoveMiddle2D3DCameraPos(MoveFrom2DTo3DCamera, Camera2D, Camera3D, ref NowState, ref IsDifferCameraStateNowOnce, IsNowChange3DCamera);
                }
                // 最終的に2Dカメラに映す場合
                else
                {
                    // 3Dカメラから2Dカメラの位置へ移動する
                    Script_CameraMove2D3D.MoveMiddle2D3DCameraPos(MoveFrom3DTo2DCamera, Camera3D, Camera2D, ref NowState , ref IsDifferCameraStateNowOnce, IsNowChange3DCamera);
                }

                break;
            }
            
            // カメラの動きを止める場合
            case CameraState.STOP:
            {
                // カメラの動きを止める
                Script_CameraStop.Stop(ref NowState, ref IsDifferCameraStateNowOnce);
                
                break;
            }
        }
    }

    /// <summary>
    /// カメラが2D⇔3Dへ切り替える準備を行う
    /// </summary>
    public void ChangeCamera2D3D()
    {
        // カメラが2D⇔3Dへ切り替え途中ではなかった場合
        if (IsMove2DCamera3DCamera == false)
        {
            // カメラが 2D ⇔ 3Dに動くように設定する
            NowState = CameraState.MOVE2D3D;

            // カメラの今と前の状態が異なる
            IsDifferCameraStateNowOnce = true;

            // 「2Dカメラと3Dカメラの間へ移動する準備を行う
            IsMove2DCamera3DCamera = true;

            // 2D ↔ 3Dカメラに切り替える
            IsNowChange3DCamera = !IsNowChange3DCamera;

            // カメラが移動する前に、プレイヤーの位置を記憶する
            PlayerObj.GetComponent<PlayerPosByCamera2D3D>().PlayerOncePos = PlayerObj.transform.position;

            // 2D ↔ 3Dカメラに切り替える際にプレイヤーがいる位置を作成する
            PlayerObj.GetComponent<PlayerPosByCamera2D3D>().CreatePlayerPosByCameraMove2D3D(IsNowChange3DCamera);

            // プレイヤーの「Rigidbody」の位置と回転を固定(フリーズ)させる
            PlayerObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>

        // カメラの今の状態 
        public CameraState State { get { return NowState; } set{ NowState = value; } }

        // カメラの今と前の状態が異なっているか
        public bool IsDifferStateNowOnce { get { return IsDifferCameraStateNowOnce; } set { IsDifferCameraStateNowOnce = value; } } 

        //  2Dカメラと3Dカメラの間へ移動するか(カメラの状態 :「2D⇔3Dの動き」→「数ミリ秒止める」が含まれる)
        public bool IsMove2D3DCameraPos { get { return IsMove2DCamera3DCamera; } set { IsMove2DCamera3DCamera = value; } }

        // 3Dカメラを表示しているか(false：2Dカメラ表示 / true：3Dカメラ表示)
        public bool IsAppearCamera3D { get { return IsNowChange3DCamera; }  set { IsNowChange3DCamera = value; } }
}

﻿// -----------------------------------------------------------------------------------------
//! @file       PlayerDeecidePosBeforeMoveCamera2D3D.cs
//!
//! @brief      カメラが2D⇔3Dへ切り替える前にプレイヤーの位置を決める処理
//!
//! @author     橋本 奉武
//!
//! @date       2019.10.23
//!
//! @note       L103で処理が遅くならないか、確認 /  L133　見直す 
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 型名を省略
/// </summary>
using Axit = Obj_OneAxitMove.Axit;

public class PlayerDeecidePosBeforeMoveCamera2D3D : MonoBehaviour
{
    // プレイヤー
     private GameObject PlayerObj = default;

    // 現在のプレイヤーのモデルサイズ
    private Vector3 PlayerObjSize;

    // プレイヤーのレイの長さでサイズによる割合
    private float RayLength_SizeRote = 0.75f;

    // プレイヤーのレイに当たったオブジェクト
    private GameObject HitObj;

    // どの軸を中心にしてプレイヤーを動かすか
    private Axit PlayerCenterAxit;

    // プレイヤーのレイに当たったオブジェクトの親はカメラの2D⇔3D切り替え時に軸中心に位置をそろえるか
    private bool IsHitObjParentMoveOneAxit = false;

    // カメラが2D⇔3Dへ切り替えるまえに、オブジェクトの表示や位置を変える処理
    private ObjStateByCameraMove2D3D Script_ObjStateByCameraMove2D3D;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // プレイヤーを探す
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーの描画用スクリプを取得する
        PlayerObjSize = PlayerObj.GetComponent<Renderer>().bounds.size;

        // カメラが2D⇔3Dへ切り替えるまえに、オブジェクトの表示や位置を変える処理 設定
        Script_ObjStateByCameraMove2D3D = GetComponent<ObjStateByCameraMove2D3D>();

    }


    /// <summary>
    /// 2Dカメラ ↔ 3Dカメラへ動く前にプレイヤーの位置を決める
    /// </summary>
    /// <param name="isnowChange3D">3Dカメラへ切り替えるか(false：2Dカメラで表示する / true：3Dカメラで表示する)</param>
    /// <param name="isinitialpos">最終的にプレイヤーが元の位置に戻るか</param>
    public void DecidePlayerPosBeforeMoveCamera2D3D(bool isnowChange3D, bool isinitialpos = false)
    {
        // =============================================================================================
        
        // プレイヤーの「Rigidbody」の位置と回転を固定(フリーズ)させる
        PlayerObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // =============================================================================================

        // プレイヤーから飛ばすレイを作成する
        Ray ray = new Ray(PlayerObj.transform.position, Vector3.down);

        // プレイヤーから飛ばしたレイに当たったオブジェクトの入れ物
        RaycastHit hit;

        // プレイヤーのモデルサイズの高さ
        float PlayerObj_Height = PlayerObjSize.y;

        // プレイヤーから飛ばすレイの長さ
        float distance = PlayerObj_Height * RayLength_SizeRote;

        // プレイヤーからレイを飛ばして何からのオブジェクトに衝突した場合
        if(Physics.Raycast(ray,out hit, distance))
        {
            //---------------------------------------------------------------------

            // 現在のオブジェクトの親
            Transform TheObject_Parent = hit.transform.parent;

            // 現在のオブジェクトの親が存在している場合
            if (TheObject_Parent != null)
            {
                // 先祖が無くなるまで探し続ける    
                while (TheObject_Parent != null)
                {
                    // 現在の先祖がカメラの切り替え時に軸中心に動かすオブジェクトの場合
                    if (TheObject_Parent.gameObject.GetComponent<Obj_OneAxitMove>() != null)
                    {
                        // プレイヤーのレイが指定されたオブジェクトに当たった場合に行う処理
                        HitTheObj(hit.transform.gameObject, hit.transform.parent.gameObject);
                    }

                    // 次の祖先を見つける
                    TheObject_Parent = TheObject_Parent.parent;
                }
            }
            else
            // このオブジェクトがカメラの切り替え時に軸中心に動かすオブジェクトの場合
            if ((hit.transform.GetComponent<Obj_OneAxitMove>() != null))
            {
                // プレイヤーのレイが指定されたオブジェクトに当たった場合に行う処理
                HitTheObj(hit.transform.gameObject, hit.transform.gameObject);
            }
        }

        // =============================================================================================

        // カメラが2D⇔3Dへ切り替えるまえに、オブジェクトの表示や位置を変える
        Script_ObjStateByCameraMove2D3D.ChangeObjByCamera(isnowChange3D);

        // =============================================================================================
       
        // プレイヤーが乗っている地面は一つの軸中心にそろえて動くものか かつ カメラが2D→3Dへ切り替え時のみ
        if((IsHitObjParentMoveOneAxit)&&(isnowChange3D==true))
        {
            // プレイヤーのレイに当たったオブジェクトのよるスクリプト
            Renderer renderer = HitObj.GetComponent<Renderer>();

            // プレイヤーのレイに当たったオブジェクトのモデルの高さ
            float hitobjheight = (renderer != null) ? HitObj.GetComponent<Renderer>().bounds.size.y : 1.0f;


            // プレイヤーをオブジェクトの上に乗せる高さ
            float heightsit = HitObj.transform.position.y + hitobjheight / 2.0f + PlayerObj_Height / 2.0f;

            // プレイヤーをオブジェクトの上に乗せる位置
            Vector3 nextplayerpos = (PlayerCenterAxit == Axit.X) ? new Vector3(HitObj.transform.position.x, heightsit, PlayerObj.transform.position.z) :   // X軸中心にそろえる
                                    (PlayerCenterAxit == Axit.Z) ? new Vector3(PlayerObj.transform.position.x, heightsit, HitObj.transform.position.z) :   // Z軸中心にそろえる
                                                                        Vector3.zero;                                                                      // Y軸中心にそろえる
            // プレイヤーをオブジェクトの上に乗せる
            PlayerObj.GetComponent<PlayerPosByCamera2D3D>().CreatePlayerPosByCameraMove2D3D(nextplayerpos);

        }
        else
        {
            // 2D ↔ 3Dカメラに切り替える際にプレイヤーがいる位置を作成する
            PlayerObj.GetComponent<PlayerPosByCamera2D3D>().CreatePlayerPosByCameraMove2D3D(isnowChange3D, isinitialpos);
        }


        // =============================================================================================

         // リセットする
        IsHitObjParentMoveOneAxit = false;

        HitObj = null;
    }

    /// <summary>
    /// プレイヤーのレイが指定されたオブジェクトに当たった場合に行う処理
    /// </summary>
    /// <param name="obj">指定されたオブジェクト</param>
    /// <param name="obj">指定されたオブジェクトの一番の親</param>
    private void HitTheObj(GameObject obj,GameObject fistparentobj)
    {
        // プレイヤーのレイに当たったオブジェクトを記憶させる
        HitObj = obj;

        // プレイヤーがどの軸を中心にそろえるか 取得する
        PlayerCenterAxit = fistparentobj.GetComponent<Obj_OneAxitMove>().TidyOneAxit;

        // プレイヤーのレイに当たったオブジェクトの親はカメラの切り替え時に一つの軸に揃える
        IsHitObjParentMoveOneAxit = true;

        // 「プレイヤーの位置によって表示非表示させるオブジェクトによる処理」のスクリプトを呼ぶ
        Obj_AppearDisPlayerPos script_Obj_AppearDisPlayerPos = fistparentobj.GetComponent<Obj_AppearDisPlayerPos>();

        // そのスクリプトが存在する場合
        if (script_Obj_AppearDisPlayerPos != null)
        {
            // 指定されたオブジェクトにプレイヤーが乗っていることにする
            script_Obj_AppearDisPlayerPos.IsPlayerRideOnTheObj = true;
        }


    }
}

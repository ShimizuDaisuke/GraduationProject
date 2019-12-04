//=======================================================================================
//! @file   Gravity.cs
//!
//! @brief  オブジェクトに重力をつける
//!
//! @author 橋本奉武
//!
//! @date   10月10日
//!
//! @note   重力の値を変えられる
//=======================================================================================
// 警告を無効にする
#pragma warning disable CS0109

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 型名を省略
using EventKind = EventDirector.EventKIND;

public class Gravity : MonoBehaviour
{
    // 定数：√2の数
    private const float SQUAREROOT_2 = 1.414213f;

    // イベントの監督
    [SerializeField]
    private GameObject EventDirectorObj = default;
    
    // 重力(目安:40.0f)  プレイヤーが地面上にいる
    [SerializeField]
    private float GravityObj_HitObj = 40.0f;

    // 重力(目安:100.0f)  プレイヤーが地面上にいない 
    [SerializeField]
    private float GravityObj_NoHitObj = 100.0f;

    // 重力の最大速度(目安:2.0f) プレイヤーが地面上にいる
    [SerializeField]
    private float MaxGravity_HitObj = 2.0f;

    // 重力の最大速度(目安:10.0f) プレイヤーが地面上にいない
    [SerializeField]
    private float MaxGravity_NoHitObj = 10.0f;

    // プレイヤー
    private GameObject PlayerObj;

    // rigidbody 
    private new Rigidbody rigidbody;

    // プレイヤーから飛ばすレイの長さ (小数点切り上げの数)
    private float PlayerRayLength = Mathf.Ceil(SQUAREROOT_2);

    // スクリプト：イベント監督用のスクリプト
    private EventDirector Script_EventDirector;

    void Start()
    {
        // プレイヤーを探す
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーのオブジェクトのrigidbodyを取得する
        rigidbody = PlayerObj.GetComponent<Rigidbody>();

        // スクリプト：イベント監督用のスクリプト 取得
        Script_EventDirector = EventDirectorObj.GetComponent<EventDirector>();

        // rigidbodyによる重力を無効にする
        rigidbody.useGravity = false;

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void FixedUpdate()
    {
        // 何かしらイベントが発生された場合
        if(Script_EventDirector.IsEventKIND != EventKind.NONE)
        {
            // rigidbodyによる重力を有効にして、自動に重力が働くようにする
            rigidbody.useGravity = true;

            // 今後の処理を飛ばす
            return;
        }

        // --------------------------------------------------------------------------------------------

        // プレイヤーからレイを飛ばす
        Ray ray = new Ray(PlayerObj.transform.position, Vector3.down);

        // プレイヤーからレイを飛ばして当たったオブジェクトの入れ物
        RaycastHit hit;

        // rigidbodyによる重力を無効にする
        rigidbody.useGravity = false;


        // プレイヤーから飛ばしたレイが何かのオブジェクトに当たった場合
        if(Physics.Raycast(ray, out hit, PlayerRayLength))
        {
            // 手動で重力を付ける
            UseGravity(GravityObj_HitObj, MaxGravity_HitObj);

            // 今後の処理を飛ばす
            return;
        }


        // -------------------------------------------------------------------------------------------

        // プレイヤーが地面上にいなかった場合、手動で常に重力が働くようにする
        UseGravity(GravityObj_NoHitObj, MaxGravity_NoHitObj);

 
    } 


    /// <summary>
    /// 手動で重力を付ける
    /// </summary>
    /// <param name="addvell">加速度</param>
    /// <param name="maxvell">最大速度</param>
    private void UseGravity(float addvell,float maxvell)
    {
        // 重力
        Vector3 gravity_vec3 = new Vector3(0.0f, -addvell, 0.0f);

        // 手動で常に重力が働くようにする
        rigidbody.AddForce(gravity_vec3, ForceMode.Acceleration);

        // 重力の速度が範囲内より越えた場合
        if (rigidbody.velocity.y < -maxvell)
        {
            // 重力の速度を制限する
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -maxvell, rigidbody.velocity.z);
        }
    }
}
